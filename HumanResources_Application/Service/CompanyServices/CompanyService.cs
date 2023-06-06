using AutoMapper;
using HumanResources_Application.Models.DTOs.AppUserDTOs;
using HumanResources_Application.Models.DTOs.CompanyDTOs;
using HumanResources_Application.Models.VMs.CompanyVMS;
using HumanResources_Domain.Entities;
using HumanResources_Domain.Enums;
using HumanResources_Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Service.CompanyServices
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IAppUserRepository _appUserRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper, UserManager<AppUser> userManager, IAppUserRepository appUserRepository)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _userManager = userManager;
            _appUserRepository = appUserRepository;
        }

        public async Task<CompanyUpdateDTO> GetById(Guid? id)
        {
            Company company = await _companyRepository.GetDefault(x => x.Id == id);

            if (company != null)
            {
                var model = _mapper.Map<CompanyUpdateDTO>(company);

                return model;
            }
            return null;
        }

        public async Task<List<CompanyVM>> GetAllCompanies()
        {
            List<CompanyVM> companies = (List<CompanyVM>)await _companyRepository.GetFilteredList(
                select: x => new CompanyVM
                {
                    Id = x.Id,
                    CompanyName = x.CompanyName,
                    CompanyEmail = x.CompanyEmail,
                    CompanyPhoneNumber = x.CompanyPhoneNumber,
                    NumberOfEmployees = x.NumberOfEmployees,
                    CreateDate = x.CreateDate.ToShortDateString(),
                    TaxNo = x.TaxNo,
                    TaxOffice = x.TaxOffice,
                    City = x.City,
                    Country = x.Country,
                    Address = x.Address,
                    PostCode = x.PostCode,
                    Status = x.Status

                },
                where: x => (x.Status == Status.Active || x.Status == Status.AwatingApproval || x.Status == Status.Passive),
                orderby: x => x.OrderBy(x => x.CompanyName));
            return companies;
        }
        public async Task Create(CompanyCreateDTO createCompanyDTO)
        {
            var model = _mapper.Map<Company>(createCompanyDTO);
            await _companyRepository.Add(model);
        }

        public async Task Update(CompanyUpdateDTO updateCompanyDTO)
        {
            var model = _mapper.Map<Company>(updateCompanyDTO);
            model.UpdateDate = updateCompanyDTO.CompanyUpdateDateTime;
            //model.Status = updateCompanyDTO.CompanyStatus;
            model.Status = Status.Active;
            await _companyRepository.Update(model);
        }

        public async Task Delete(Guid id)
        {
            Company company = await _companyRepository.GetDefault(x => x.Id == id);

            if (company != null)
            {
                company.DeleteDate = DateTime.Now;
                company.Status = Status.Passive;

                await _companyRepository.Delete(company);
            }
        }

        public async Task<Guid?> GetCompanyId(string name)
        {
            var companyId = await _userManager.FindByNameAsync(name);
            return companyId.CompanyId;
        }

        public async Task AktiveCompany(Guid? id)
        {
            AppUser user = await _appUserRepository.GetDefault(x => x.CompanyId == id);
            user.Status = Status.Active;
            Company company = await _companyRepository.GetDefault(x => x.Id == id);
            company.Status = Status.Active;

            if (user != null && company != null)
            {
                var model = _mapper.Map<AppUser>(user);
                var model1 = _mapper.Map<Company>(company);
                await _appUserRepository.Update(model);
                await _companyRepository.Update(model1);

            }
        }

        public async Task DeAktiveCompany(Guid? id)
        {
            AppUser user = await _appUserRepository.GetDefault(x => x.CompanyId == id);
            user.Status = Status.Passive;
            Company company = await _companyRepository.GetDefault(x => x.Id == id);
            company.Status = Status.Passive;

            if (user != null && company != null)
            {
                var model = _mapper.Map<AppUser>(user);
                var model1 = _mapper.Map<Company>(company);
                await _appUserRepository.Update(model);
                await _companyRepository.Update(model1);

            }
        }

        public async Task<List<CompanyChartVM>> GetAllCompaniesPieChart()
        {
            List<CompanyChartVM> CompaniesDistributionBySectors = new List<CompanyChartVM>();
            var companies = await _companyRepository.GetFilteredList(
            select: x => new CompanyVM()
            {
                CompanyName = x.CompanyName,
            },
            where: null
            );
            double companyCount = companies.Count; //Bütün companyleri getiriyoruz sayısını bulmak için
            for (int i = 1; i <= Enum.GetValues(typeof(Status)).Length; i++)
            {
                if (i == 1)
                {
                    var tempCompanies = await _companyRepository.GetFilteredList(
                    select: x => new CompanyVM()
                    {
                        CompanyName = x.CompanyName
                    },
                    where: x => x.Status == Status.Active,
                    orderby: null
                    );
                    if (tempCompanies.Count != 0)
                    {
                        double ratio = (tempCompanies.Count / companyCount) * 100;
                        CompaniesDistributionBySectors.Add(new CompanyChartVM($"{Status.Active} ({tempCompanies.Count})", Math.Round(ratio, 2)));
                    }
                }
                else if (i == 2)
                {
                    var tempCompanies = await _companyRepository.GetFilteredList(
               select: x => new CompanyVM()
               {
                   CompanyName = x.CompanyName
               },
               where: x => x.Status == Status.Passive,
               orderby: null
               );
                    if (tempCompanies.Count != 0)
                    {
                        double ratio = (tempCompanies.Count / companyCount) * 100;
                        CompaniesDistributionBySectors.Add(new CompanyChartVM($"{Status.Passive} ({tempCompanies.Count})", Math.Round(ratio, 2)));
                    }
                }
                else if (i == 3)
                {
                    var tempCompanies = await _companyRepository.GetFilteredList(
               select: x => new CompanyVM()
               {
                   CompanyName = x.CompanyName
               },
               where: x => x.Status == Status.AwatingApproval,
               orderby: null
               );
                    if (tempCompanies.Count != 0)
                    {
                        double ratio = (tempCompanies.Count / companyCount) * 100;
                        CompaniesDistributionBySectors.Add(new CompanyChartVM($"{Status.AwatingApproval} ({tempCompanies.Count})", Math.Round(ratio, 2)));
                    }
                }
            }
            return CompaniesDistributionBySectors;
        }

        public async Task<List<CompanyChartVM>> GetAllCompaniesBarChart()
        {
            List<CompanyChartVM> CompaniesDistributionByYears = new List<CompanyChartVM>();
            var companies = await _companyRepository.GetFilteredList(
            select: x => new CompanyVM()
            {
                CompanyName = x.CompanyName,
                NumberOfEmployees = x.NumberOfEmployees

            },
            where: null,
            orderby: x => x.OrderBy(x => x.CompanyName)
            );
            

            foreach (var item in companies)
            {
                CompaniesDistributionByYears.Add(new CompanyChartVM($"{item.CompanyName}", item.NumberOfEmployees));
                    
            }
            return CompaniesDistributionByYears;
        }
    }
}
