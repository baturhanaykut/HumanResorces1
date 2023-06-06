using HumanResources_Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Domain.Entities
{
    public class LeaveRequest : IBaseEntity
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? LeaveDay { get; set; }

        /*public DateTime DateRequested { get; set; }*/ // izin istemenin yapildigi tarih
        public string RequestComments { get; set; }

        /*public DateTime? DateActioned { get; set; }*/
        public ApproveStatus? ApproveStatus { get; set; }




        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }




        //Navigation Property:
        //public Guid ApprovedById { get; set; }
        //public AppUser ApprovedByAppUser { get; set; }

        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public LeaveType LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
    }
}
