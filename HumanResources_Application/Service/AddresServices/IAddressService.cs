using HumanResources_Application.Models.VMs.AddressVMs;
using HumanResources_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Service.AddresServices
{
    public interface IAddressService
    {
        Task<List<CityVM>> GetCities();
        Task<List<DistrictVM>> GetDistricts();

        Task<List<DistrictVM>> GetDistricts(int cityId);

        Task<CityVM> GetCityById(int cityId);

    }
}
