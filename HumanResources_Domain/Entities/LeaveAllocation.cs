using HumanResources_Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Domain.Entities
{
    public class LeaveAllocation : IBaseEntity
    {
        public int Id { get; set; }
        public int NumberOfDays { get; set; }
        public int Period { get; set; }

        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public LeaveType LeaveType { get; set; }
        public int LeaveTypeId { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }



    }
}
