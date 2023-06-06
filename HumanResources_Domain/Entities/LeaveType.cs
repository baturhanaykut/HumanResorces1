using HumanResources_Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Domain.Entities
{
    public class LeaveType : IBaseEntity
    {
        public LeaveType()
        {
            LeaveRequests = new HashSet<LeaveRequest>();
        }

        public int Id { get; set; }
        public string LeaveTypeName { get; set; }
        public decimal? DefaultDays { get; set; }


        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

        public Guid? CompanyId { get; set; }

        //Navigation Property
        public Company? Company { get; set; }
        public virtual ICollection<LeaveRequest> LeaveRequests { get; set; }


    }
}
