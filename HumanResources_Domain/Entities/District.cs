using HumanResources_Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Domain.Entities
{
    public class District : IBaseEntity
    {
        public District()
        {
            Adresses = new HashSet<Address>();
        }
        public int Id { get; set; }

        public string DistrictName { get; set; }

        public int CityId { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }


        //Navigation Property
        public virtual ICollection<Address> Adresses { get; set; }
        //Todo Config
        public City City { get; set; }
    }
}
