using HumanResources_Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Domain.Entities
{
    public class City : IBaseEntity
    {
        public City()
        {
            Districts = new HashSet<District>();
        }

        public int Id { get; set; }

        public string CityName { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

        //Navigation Property

        public virtual ICollection<District> Districts { get; set; }
        
        

    }
}
