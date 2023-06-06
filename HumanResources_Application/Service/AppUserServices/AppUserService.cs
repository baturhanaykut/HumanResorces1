using AutoMapper;
using HumanResources_Application.Models.DTOs.AppUserDTOs;
using HumanResources_Application.Models.VMs.AddressVMs;
using HumanResources_Application.Models.VMs.AppUserVMs;
using HumanResources_Application.Service.CompanyServices;
using HumanResources_Domain.Entities;
using HumanResources_Domain.Enums;
using HumanResources_Domain.Repositories;
using HumanResources_Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HumanResources_Application.Service.AppUserServices
{
    public class AppUserService : IAppUserService
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;



        public AppUserService(IAppUserRepository appUserRepository, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IMapper mapper, IAddressRepository addressRepository, ICompanyRepository companyRepository)
        {
            _appUserRepository = appUserRepository;
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _addressRepository = addressRepository;
            _companyRepository = companyRepository;

        }

        public async Task<UpdateProfileDTO> GetByUserName(string userName)
        {
            UpdateProfileDTO result = await _appUserRepository.GetFiltredFirstOrDefault(
                select: x => new UpdateProfileDTO
                {

                    Id = x.Id,
                    UserName = x.UserName,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    IdentityNumber = x.IdentityNumber,
                    BloodGroup = x.BloodGroup.Value,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    Gender = x.Gender.Value,
                    AddressDescription = x.Address.Description,
                    CityId = x.Address.District.CityId,
                    CityName = x.Address.District.City.CityName,
                    DistrictId = (int)x.Address.DistrictId,
                    DistrictName = x.Address.District.DistrictName,
                    Title = x.Title,
                    Job = x.Job,
                    ImagePath = x.ImagePath,
                    BirthDate = x.BirthDate,
                    StartWorkDate = x.StartWorkDate,
                    DepartmentId = x.Department.Id,
                    DepartmentName = x.Department.Name,
                    FullName = x.FullName,
                    ExecutiveId = x.ExecutiveId,
                    ExecutiveName = x.Executive.FullName,
                    ExecutiveStatus = (bool)x.ExecutiveStatus,
                },
                where: x => x.UserName == userName,
                orderby: null,
                include: x => x.Include(x => x.Department).Include(x => x.Address).Include(x => x.Address.District).Include(x => x.Address.District.City)
                );
            return result;

        }

        public async Task<SignInResult> Login(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            bool result = await IsSiteManager(user.Email);

            if (result == true)
            {
                if (user != null)
                {
                    return await _signInManager.PasswordSignInAsync(user.Email, model.Password, false, false);
                }
            }
            else
            {
                var company = await _companyRepository.GetDefault(x => x.Id == (Guid)user.CompanyId);

                if ((user.Status == Status.Active) && (company.Status == Status.Active))
                {
                    if (user != null)
                    {
                        return await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false);
                    }
                }
            }

            return await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<RegisterVM> Register(RegisterDTO model)
        {
            RegisterVM register = new RegisterVM();

            Company company = _mapper.Map<Company>(model);
            company.Id = Guid.NewGuid();
            company.Status = Status.AwatingApproval;


            AppUser user = _mapper.Map<AppUser>(model);
            user.UserName = user.Email;
            user.CompanyId = company.Id;
            user.Status = Status.AwatingApproval;
            user.ExecutiveStatus = false;


            if (model.Password == model.ConfirmPassword)
            {
                await _companyRepository.Add(company);
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "CompanyManager");
                }

                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    register.Email = user.Email;
                    register.Token = token;
                    register.Result = result;
                    register.Password = model.Password;
                }
                else
                {
                    register.Result = result;
                }


            }
            return register;
        }

        public async Task UpdateUser(UpdateProfileDTO model)
        {
            AppUser user = await _appUserRepository.GetDefault(x => x.Id == model.Id);
            Address useraddress = await _addressRepository.GetDefault(x => x.AppUserId == model.Id);

            if (model.UploadPath != null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());

                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/media/image/{guid}.jpg");

                user.ImagePath = $"/media/image/{guid}.jpg";
            }
            else
            {
                if (model.ImagePath != null)
                {
                    user.ImagePath = model.ImagePath;
                }
                else
                {
                    user.ImagePath = $"/images/defaultpost.jpg";

                }
            }

            if (model.Password != null)
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);

                await _userManager.UpdateAsync(user);
            }

            if (model.Email != null)
            {
                AppUser isuserEmailExists = await _userManager.FindByEmailAsync(model.Email);
                if (isuserEmailExists == null)
                {
                    await _userManager.SetEmailAsync(user, model.Email);
                }
            }

            if (model.Role != null)
            {
                var currentRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, currentRoles.ToArray());
                await _userManager.AddToRoleAsync(user, model.Role);
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.IdentityNumber = model.IdentityNumber;
            user.BloodGroup = model.BloodGroup;
            user.PhoneNumber = model.PhoneNumber;
            user.Gender = model.Gender;

            if (model.DistrictId != null && model.CityId != null)
            {
                if (useraddress == null)
                {
                    user.Address = new Address()
                    {
                        CreateDate = DateTime.Now,
                        Description = model.AddressDescription,
                        DistrictId = (int)model.DistrictId
                    };

                }
                else
                {
                    if (model.DistrictId != null)
                    {
                        useraddress.DistrictId = model.DistrictId;
                    }

                    useraddress.UpdateDate = DateTime.Now;
                    useraddress.Description = model.AddressDescription;
                }
            }

            user.Title = model.Title;
            user.Job = model.Job;
            user.UpdateDate = DateTime.Now;
            user.Status = Status.Active;    //Modified'tan aktife çekildi.
            user.BirthDate = model.BirthDate;
            user.StartWorkDate = model.StartWorkDate;
            user.DepartmentId = model.DepartmentId;
            user.ExecutiveId = model.ExecutiveId;
            user.ExecutiveStatus = model.ExecutiveStatus;           

            await _appUserRepository.Update(user);
        }

        public async Task<IList<string>> GetUserRole(string email)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.GetRolesAsync(user);
            return result;
        }

        public async Task<bool> IsSiteManager(string email)
        {
            var roles = await GetUserRole(email);

            foreach (var role in roles)
            {
                if (role == "SiteManager")
                    return true;
            }
            return false;
        }

        public async Task<bool> IsCompanyManager(string email)
        {
            var roles = await GetUserRole(email);

            foreach (var role in roles)
            {
                if (role == "CompanyManager")
                    return true;
            }
            return false;
        }

        public async Task<bool> IsEmployee(string email)
        {
            var roles = await GetUserRole(email);

            foreach (var role in roles)
            {
                if (role == "Employee")
                    return true;
            }
            return false;
        }

        public async Task<List<EmployeeVM>> GetEmployees(Guid? Id)
        {
            List<EmployeeVM> employees = (List<EmployeeVM>)await _appUserRepository.GetFilteredList(
                select: x => new EmployeeVM()
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    IdentityNumber = x.IdentityNumber,
                    BloodGroup = x.BloodGroup.Value,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    Gender = x.Gender.Value,
                    AddressDescription = x.Address.Description,
                    CityId = x.Address.District.CityId,
                    CityName = x.Address.District.City.CityName,
                    DistrictId = (int)x.Address.DistrictId,
                    Title = x.Title,
                    Job = x.Job,
                    ImagePath = x.ImagePath,
                    BirthDate = x.BirthDate.ToShortDateString(),
                    StartWorkDate = x.StartWorkDate.ToShortDateString(),
                    DepartmanId = x.Department.Id,
                    DepartmanName = x.Department.Name,
                    FullName = x.FullName,
                    ExecutiveId = x.ExecutiveId,
                    ExecutiveName = x.Executive.FullName,
                    ExecutiveStatus = x.ExecutiveStatus,
                    CompanyId = x.CompanyId,
                    Status = x.Status

                },
                where: x => (x.Status != Status.Passive) && (x.CompanyId == Id),
                orderby: x => x.OrderBy(x => x.FirstName),
                include: x => x.Include(x => x.Department).Include(x => x.Address).Include(x => x.Address.District).Include(x => x.Address.District.City)
                ); ;

            return employees;
        }

        public async Task<CreateEmployeeVM> CreateUser(CreateEmployeeDTO model)
        {
            var newEmployee = _mapper.Map<AppUser>(model);

            newEmployee.UserName = model.Email;
            newEmployee.Status = Status.AwatingApproval;

            var companyManger = await _appUserRepository.GetDefault(x => x.Id == model.ExecutiveId);
            newEmployee.CompanyId = companyManger.CompanyId;

            Random randompassword = new Random();
            model.Password = randompassword.Next(100000, 999999).ToString();
            model.ConfirmPassword = model.Password;

            if (model.DistrictId != 0 && model.CityId != 0)
            {
                newEmployee.Address = new Address()
                {
                    CreateDate = DateTime.Now,
                    Description = model.AddressDescription,
                    DistrictId = (int)model.DistrictId
                };

            }

            var result = await _userManager.CreateAsync(newEmployee, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newEmployee, model.Role);
            }

            CreateEmployeeVM register = new CreateEmployeeVM();
            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(newEmployee);
                register.Email = newEmployee.Email;
                register.Token = token;
                register.Result = result;
                register.Password = model.Password;
            }
            else
            {
                register.Result = result;
            }
            return register;

        }

        public async Task<EmployeeVM> GetEmployee(string userName)
        {
            var employee = await _appUserRepository.GetFiltredFirstOrDefault(
                select: x => new EmployeeVM()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    DepartmanName = x.Department.Name,
                    StartWorkDate = x.StartWorkDate.ToShortDateString(),
                    FullName = x.FullName,
                    ExecutiveId = x.ExecutiveId,
                    ExecutiveName = x.Executive.FullName,
                    ExecutiveStatus = x.ExecutiveStatus,
                    CompanyId = x.CompanyId,
                    Status = x.Status,
                    //ImagePath = x.ImagePath == null ? "/media/image/defaultprofilepic.png" : x.ImagePath,
                },
                where: x => x.UserName == userName,
                include: x => x.Include(x => x.Department)
                );

            return employee;
        }

        public async Task<EmployeeVM> GetEmployeeId(Guid? Id)
        {
            var employee = await _appUserRepository.GetFiltredFirstOrDefault(
                select: x => new EmployeeVM()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    DepartmanName = x.Department.Name,
                    StartWorkDate = x.StartWorkDate.ToShortDateString(),
                    FullName = x.FullName,
                    ExecutiveId = x.ExecutiveId,
                    ExecutiveName = x.Executive.FullName,
                    CompanyId = x.CompanyId,
                    Status = x.Status
                    //ImagePath = x.ImagePath == null ? "/media/image/defaultprofilepic.png" : x.ImagePath,
                },
                where: x => x.Id == Id,
                include: x => x.Include(x => x.Department)
                );

            return employee;
        }

        public async Task Delete(Guid? Id)
        {
            AppUser user = await _appUserRepository.GetDefault(x => x.Id == Id);

            if (user != null)
            {
                user.DeleteDate = DateTime.Now;
                user.Status = Status.Passive;
                await _appUserRepository.Delete(user);
            }

        }

        public async Task<IdentityResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var usercompany = await _companyRepository.GetDefault(x => x.Id == user.CompanyId);

            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                user.Status = Status.Active;
                //usercompany.Status = Status.Active;
                await _appUserRepository.Save();
                return result;
            }
            return IdentityResult.Failed();
        }

        public async Task<Guid> GetUserId(string userName)
        {
            AppUser user = await _userManager.FindByNameAsync(userName);
            return user.Id;
        }

        public async Task<List<EmployeeVM>> GetExecutive(Guid? id)
        {
            List<EmployeeVM> executive = (List<EmployeeVM>)await _appUserRepository.GetFilteredList(
                select: x => new EmployeeVM()
                {
                    Id = x.Id,
                    FullName = x.FullName,

                },
                where: x => x.CompanyId == id,
                orderby: x => x.OrderBy(x => x.FirstName)
                );
            return executive;
        }

        public async Task<List<EmployeeExcelVM>> GetEmployeesForExcel(Guid? Id)
        {
            List<EmployeeExcelVM> employees = (List<EmployeeExcelVM>)await _appUserRepository.GetFilteredList(
                select: x => new EmployeeExcelVM()
                {
                    FullName = x.FullName,
                    Email = x.Email,
                    IdentityNumber = x.IdentityNumber,
                    DepartmanName = x.Department.Name,
                    Title = x.Title,
                    PhoneNumber = x.PhoneNumber,
                    StartWorkDate = x.StartWorkDate.ToShortDateString(),
                },
                where: x => (x.Status != Status.Passive) && (x.CompanyId == Id),
                orderby: x => x.OrderBy(x => x.FirstName),
                include: x => x.Include(x => x.Department).Include(x => x.Address).Include(x => x.Address.District).Include(x => x.Address.District.City)
                );

            return employees;
        }

        public async Task<ForgotPasswordVM> ForgotPassword(string email)
        {
            ForgotPasswordVM forgotPasswordVM = new ForgotPasswordVM();

            AppUser user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                forgotPasswordVM.Result = IdentityResult.Failed();
                return forgotPasswordVM;
            }

            string code = await _userManager.GeneratePasswordResetTokenAsync(user);

            forgotPasswordVM.Email = user.Email;
            forgotPasswordVM.Token = code;
            forgotPasswordVM.Result = IdentityResult.Success;

            return forgotPasswordVM;
        }
    }
}
