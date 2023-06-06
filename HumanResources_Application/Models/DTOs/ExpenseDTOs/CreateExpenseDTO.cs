using HumanResources_Application.Extensions;
using HumanResources_Application.Models.VMs.ExpenseTypeVMs;
using HumanResources_Domain.Entities;
using HumanResources_Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Models.DTOs.ExpenseDTOs
{
    public class CreateExpenseDTO
    {
        [Required(ErrorMessage = "Explanation field cannot be empty!")]
        [MaxLength(100)]
        public string Explanation { get; set; }

        [Required(ErrorMessage = "Amount field cannot be empty!")]
        [Range(0, 99999.99, ErrorMessage = "Please enter between 0-99999.99!")]
        public decimal ExpenseAmount { get; set; }

        [Required(ErrorMessage = "Expense Date area cannot be empty!")]
        public DateTime ExpenseDate { get; set; }

        public DateTime CreateDate => DateTime.Now;

        [ValidateNever]
        public Status Status => Status.Active;

        public Guid? AppUserId { get; set; }

        public Guid? ExecutiveId { get; set; }

        public ApproveStatus ApproveStatus => ApproveStatus.InApproval;

        [ValidateNever]
        public string ExpenseDocument { get; set; }

        [ValidateNever]
        [PictureFileExtention]
        public IFormFile? UploadExpenseDocument { get; set; }

        [Required(ErrorMessage = "Expense types cannot be empty!")]
        public int? ExpenseTypeId { get; set; }

        public virtual ICollection<ExpenseTypeVM>? ExpenseTypes { get; set; }
    }
}
