using HumanResources_Application.Models.DTOs.AppUserDTOs;
using HumanResources_Application.Models.DTOs.CompanyDTOs;
using HumanResources_Application.Models.VMs.CompanyVMS;
using HumanResources_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Service.CompanyServices
{
    public interface ICompanyService
    {
        Task Create(CompanyCreateDTO createCompanyDTO);
        Task Update(CompanyUpdateDTO updateCompanyDTO);
        Task Delete(Guid id);
        Task<List<CompanyVM>> GetAllCompanies();
        
        Task<CompanyUpdateDTO> GetById(Guid? id);

        Task<Guid?> GetCompanyId(string name);

        Task AktiveCompany(Guid? id);

        Task DeAktiveCompany(Guid? id);

        Task<List<CompanyChartVM>> GetAllCompaniesPieChart();
        Task<List<CompanyChartVM>> GetAllCompaniesBarChart();
    }
}
