using HumanResources_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HumanResources_Domain.Entities;
using System.Linq.Expressions;
using HumanResources_Application.Models.VMs.CompanyVMS;
using HumanResources_Application.Models.VMs.AppUserVMs;

namespace HumanResources_Application.Service.SearchServices
{
    public interface ISearchService
    {
        Task<List<EmployeeVM>> GetSearchResult(string employeName, Guid? companyId);
    }
}
