using HumanResources_Domain.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Models.DTOs.LeaveTypeDTOs
{
    public class UpdateLeaveTypeDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Leave type field cannot be empty!")]
        [MaxLength(50)]
        public string LeaveTypeName { get; set; }

        [Required(ErrorMessage = "Default days field cannot be empty!")]
        public decimal? DefaultDays { get; set; }

        //public DateTime CreateDate;

        public DateTime? UpdateDate => DateTime.Now;

        [ValidateNever]
        public Status Status => Status.Active;
    }
}
