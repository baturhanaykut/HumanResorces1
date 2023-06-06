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
    public class UpdateExpenseDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Explanation area cannot be empty!")]
        [MaxLength(50)]
        public string Explanation { get; set; }


        [Required(ErrorMessage = "Amount area cannot be empty!")]
        [Range(0, 99999.99, ErrorMessage = "Please input between 0-99999.99!")]
        public decimal ExpenseAmount { get; set; }

        [Required(ErrorMessage = "Expense Date area cannot be empty!")]
        public DateTime ExpenseDate { get; set; }

        public DateTime? UpdateDate => DateTime.Now;

        public DateTime CreateDate { get; set; }

        public Status Status => Status.Active; //Modified'tan aktife çekildi.

        public ApproveStatus ApproveStatus { get; set; }

        public Guid? ExecutiveId { get; set; }

        public Guid AppUserId { get; set; }


        [ValidateNever]
        public string ExpenseDocument { get; set; }

        [ValidateNever]
        [PictureFileExtention]
        public IFormFile? UploadExpenseDocument { get; set; }

        public int? ExpenseTypeId { get; set; }

        public virtual ICollection<ExpenseTypeVM>? ExpenseTypes { get; set; }
    }
}
