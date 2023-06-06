using HumanResources_Domain.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Models.DTOs.ExpenseTypeDTOs
{
    public class CreateExpenseTypeDTO
    {
        [Required(ErrorMessage = "Explanation field cannot be empty!")]
        [MaxLength(50)]
        public string ExpenseTypeName { get; set; }

        public DateTime CreateDate => DateTime.Now;

        [ValidateNever]
        public Status Status => Status.Active;
    }
}
