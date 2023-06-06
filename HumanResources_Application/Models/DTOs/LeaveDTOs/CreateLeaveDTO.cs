
using HumanResources_Application.Models.VMs.LeaveTypeVMs;
using HumanResources_Domain.Entities;
using HumanResources_Domain.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Models.DTOs.LeaveDTOs
{
    public class CreateLeaveDTO
    {
        [Required(ErrorMessage = "Start date area cannot be empty!")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date area cannot be empty!")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Leave day area cannot be empty!")]
        public decimal? LeaveDay { get; set; }

        [Required(ErrorMessage = "Comments field cannot be empty!")]
        [MaxLength(50)]
        public string RequestComments { get; set; }

        public ApproveStatus ApproveStatus => ApproveStatus.InApproval;

        public DateTime CreateDate => DateTime.Now;

        [ValidateNever]
        public Status Status => Status.Active;

        public Guid? AppUserId { get; set; }
        public Guid? ExecutiveId { get; set; }

        [Required(ErrorMessage = "Leave types cannot be empty!")]
        public int? LeaveTypeId { get; set; }

        public virtual ICollection<LeaveTypeVM>? LeaveTypes { get; set; }
    }
}
