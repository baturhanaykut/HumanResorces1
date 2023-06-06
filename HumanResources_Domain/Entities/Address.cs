using HumanResources_Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Domain.Entities
{
    public class Address:IBaseEntity
    {
   
        public int AddressId { get; set; }
        public string? Description { get; set; }
        public int? PostCode { get; set; }
        public Guid? AppUserId { get; set; }
        public int? DistrictId { get; set; }
        
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }
        
        
        //Navigation Property
             
        public District? District { get; set; }
        public AppUser? AppUser { get; set; }
        
    }
}
