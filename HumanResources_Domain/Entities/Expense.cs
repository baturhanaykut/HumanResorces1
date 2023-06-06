using HumanResources_Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Domain.Entities
{
    public class Expense : IBaseEntity
    {
        public int Id { get; set; }
        public string Explanation { get; set; }
        public decimal ExpenseAmount { get; set; }
        public ApproveStatus ApproveStatus { get; set; }
        public DateTime ExpenseDate { get; set; }
        public Guid AppUserId { get; set; }

        public string? ExpenseDocument { get; set; }
        [NotMapped]
        public IFormFile UploadExpenseDocument { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

        public int ExpenseTypeId { get; set; }

        //Navigation Properties
        public AppUser AppUser { get; set; }

        public ExpenseType ExpenseType { get; set; }
    }
}
