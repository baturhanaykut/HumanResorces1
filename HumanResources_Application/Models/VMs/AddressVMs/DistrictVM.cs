using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Models.VMs.AddressVMs
{
    public class DistrictVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? CityId { get; set; }
    }
}
