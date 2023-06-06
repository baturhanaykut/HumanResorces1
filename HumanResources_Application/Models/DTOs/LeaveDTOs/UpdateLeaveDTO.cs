using HumanResources_Application.Models.VMs.LeaveTypeVMs;
using HumanResources_Domain.Entities;
using HumanResources_Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Models.DTOs.LeaveDTOs
{
    public class UpdateLeaveDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Start Date area cannot be empty!")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date area cannot be empty!")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Leave day area cannot be empty!")]
        public decimal? LeaveDay { get; set; }

        [Required(ErrorMessage = "Comments field cannot be empty!")]
        [MaxLength(50)]
        public string RequestComments { get; set; }
        public ApproveStatus ApproveStatus { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate => DateTime.Now;

        public Status Status => Status.Active; //Modified'tan aktife çekildi.

        public Guid? AppUserId { get; set; }
        public Guid? ExecutiveId { get; set; }

        public int? LeaveTypeId { get; set; }

        public virtual ICollection<LeaveTypeVM>? LeaveTypes { get; set; }
    }
}
