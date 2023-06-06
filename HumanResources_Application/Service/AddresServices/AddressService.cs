using HumanResources_Application.Models.VMs.AddressVMs;
using HumanResources_Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Service.AddresServices
{
    public class AddressService : IAddressService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IDistrictRepository _districtRepository;

        public AddressService(ICityRepository cityRepository, IDistrictRepository districtRepository)
        {
            _cityRepository = cityRepository;
            _districtRepository = districtRepository;
        }



        public async Task<List<CityVM>> GetCities()
        {
            List<CityVM> cities = (List<CityVM>)await _cityRepository.GetFilteredList(
                select: x => new CityVM()
                {
                    Id = x.Id,
                    Name = x.CityName
                },
                where: null,
                orderby: x => x.OrderBy(x => x.CityName)
                );
            return cities;
        }

        public async Task<CityVM> GetCityById(int cityId)
        {
          var cityName  = await _cityRepository.GetFiltredFirstOrDefault(
                select: x => new CityVM()
                {
                    Name = x.CityName,
                },
                where: x => x.Id == cityId,
                orderby: null,
                include: null
                );

            return cityName;
        }

        public async Task<List<DistrictVM>> GetDistricts(int cityId)
        {
            List<DistrictVM> district = (List<DistrictVM>)await _districtRepository.GetFilteredList(
                select: x => new DistrictVM()
                {
                    Id = x.Id,
                    Name = x.DistrictName,
                    CityId = x.CityId
                },
                where: x=>x.CityId == cityId,
                orderby: x => x.OrderBy(x => x.DistrictName),
                include: null
                );

            return district.ToList();
        }

        public async Task<List<DistrictVM>> GetDistricts()
        {
            List<DistrictVM> district = (List<DistrictVM>)await _districtRepository.GetFilteredList(
               select: x => new DistrictVM()
               {
                   Id = x.Id,
                   Name = x.DistrictName,
                   CityId = x.CityId
               },
               where: null,
               orderby: x => x.OrderBy(x => x.DistrictName),
               include: null
               );

            return district.ToList();
        }
    }
}
