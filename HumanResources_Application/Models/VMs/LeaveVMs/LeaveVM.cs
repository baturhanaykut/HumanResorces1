using HumanResources_Domain.Entities;
using HumanResources_Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Models.VMs.LeaveVMs
{
    public class LeaveVM
    {
        public int? Id { get; set; }

        [DisplayName("Start Date")]
        public string? StartDate { get; set; }

        [DisplayName("End Date")]
        public string? EndDate { get; set; }

        [DisplayName("Leave Day")]
        public decimal? LeaveDay { get; set; }

        public string? RequestComments { get; set; }

        [DisplayName("Approve Status")]
        public ApproveStatus? ApproveStatus { get; set; }

        [DisplayName("Create Date")]
        public string? CreateDate { get; set; }

        [DisplayName("Update Date")]
        public string? UpdateDate { get; set; }
        public Status? Status { get; set; }

        public string? FullName { get; set; }
        public Guid? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public Guid? ExecutiveId { get; set; }

        [DisplayName("Executive Name")]
        public string? ExecutiveName { get; set; }
        public int? LeaveTypeId { get; set; }
        public string? LeaveTypeName { get; set; }


    }
}
