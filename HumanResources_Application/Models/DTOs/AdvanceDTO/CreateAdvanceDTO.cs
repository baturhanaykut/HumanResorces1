using HumanResources_Domain.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Models.DTOs.AdvanceDTO
{
    public class CreateAdvanceDTO
    {
        [Required(ErrorMessage = "Explanation field cannot be empty!")]
        [MaxLength(100)]
        public string Explanation { get; set; }

        [Required(ErrorMessage = "Amount field cannot be empty!")]
        [Range(0, 99999.99, ErrorMessage = "Please enter between 0-99999.99!")]
        public decimal AdvanceAmount { get; set; }

        public DateTime PaymentDueDate { get; set; }

        public DateTime CreateDate => DateTime.Now;

        [ValidateNever]
        public Status Status => Status.Active;

        public Guid? ExecutiveId { get; set; } // Null geliyor.

        public ApproveStatus ApproveStatus => ApproveStatus.InApproval;
    }
}
