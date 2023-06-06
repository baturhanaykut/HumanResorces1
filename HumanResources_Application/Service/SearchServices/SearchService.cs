using AutoMapper;
using HumanResources_Application.Models.VMs.AppUserVMs;
using HumanResources_Application.Models.VMs.CompanyVMS;
using HumanResources_Domain.Entities;
using HumanResources_Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HumanResources_Application.Service.SearchServices
{
    public class SearchService : ISearchService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IMapper _mapper;

        public SearchService(ICompanyRepository companyRepository, IMapper mapper, IAppUserRepository appUserRepository)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _appUserRepository = appUserRepository;
        }

        public async Task<List<EmployeeVM>> GetSearchResult(string employeName, Guid? companyId)
        {

            var result = await _appUserRepository.GetDefaults(x=>x.CompanyId == companyId && ( x.UserName.ToLower().Replace("ı", "i").Replace("ç", "c").Replace("ü", "u").Replace("ğ", "g").Replace("ş", "s").Replace("ö", "o").Contains(NormalizeSearchString(employeName)) || x.FirstName.ToLower().Replace("ı", "i").Replace("ç", "c").Replace("ü", "u").Replace("ğ", "g").Replace("ş", "s").Replace("ö", "o").Contains(NormalizeSearchString(employeName))));

            //var result2 = await _appUserRepository.GetDefaults(x => NormalizeSearchString(x.UserName).Contains(NormalizeSearchString(employeName)) || NormalizeSearchString(x.FirstName).Contains(NormalizeSearchString(employeName)));

            List<EmployeeVM> model = _mapper.Map<List<EmployeeVM>>(result);

            return (model);

        }

        public string NormalizeSearchString(string text)
        {
            return text.ToLower().Replace("ı", "i").Replace("ç", "c").Replace("ü", "u").Replace("ğ", "g").Replace("ş", "s").Replace("ö", "o");

        }
    }
}
