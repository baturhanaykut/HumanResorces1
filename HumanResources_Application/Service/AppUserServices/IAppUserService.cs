using HumanResources_Application.Models.DTOs.AppUserDTOs;
using HumanResources_Application.Models.VMs.AppUserVMs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Service.AppUserServices
{
    public interface IAppUserService
    {
        Task<RegisterVM> Register(RegisterDTO model);
        Task<SignInResult> Login(LoginDTO model);
        Task<UpdateProfileDTO> GetByUserName(string userName);
        Task<List<EmployeeVM>> GetEmployees(Guid? Id);
        Task UpdateUser(UpdateProfileDTO model);
        Task LogOut();
        Task<IList<string>> GetUserRole(string email);
        Task<bool> IsSiteManager(string email);
        Task<bool> IsCompanyManager(string email);
        Task<bool> IsEmployee(string email);
        Task<CreateEmployeeVM> CreateUser(CreateEmployeeDTO model);
        Task<EmployeeVM> GetEmployee(string userName);
        Task Delete(Guid? Id);
        Task<IdentityResult> ConfirmEmail(string token, string email);

        Task<Guid> GetUserId(string userName);

        Task<List<EmployeeVM>> GetExecutive(Guid? id);

        Task<List<EmployeeExcelVM>> GetEmployeesForExcel(Guid? Id);

        Task<ForgotPasswordVM> ForgotPassword(string email);
        Task<EmployeeVM> GetEmployeeId(Guid? Id);


    }
}
