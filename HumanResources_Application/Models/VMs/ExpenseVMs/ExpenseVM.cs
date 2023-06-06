using HumanResources_Domain.Entities;
using HumanResources_Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Models.VMs.ExpenseVMs
{
    public class ExpenseVM
    {
        public int? Id { get; set; }

        [DisplayName("Expense Amount")]
        public decimal? ExpenseAmount { get; set; }

        [DisplayName("Approve Status")]
        public ApproveStatus? ApproveStatus { get; set; }

        [DisplayName("Request Date")]
        public string CreateDate { get; set; }

        [DisplayName("Update Date")]
        public DateTime? UpdateDate { get; set; }

        public Status? Status { get; set; }

        public string? AppUserName { get; set; }

        public string? Explanation { get; set; }

        [DisplayName("Expense Date")]
        public string ExpenseDate { get; set; }

        [DisplayName("Expense Document")]
        public string? ExpenseDocument { get; set; }

        public Guid? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        [DisplayName("Full Name")]
        public string? FullName { get; set; }

        //Yönetici getirmek için:
        public Guid? ExecutiveId { get; set; }

        [DisplayName("Executive Name")]
        public string? ExecutiveName { get; set; }

        public int? ExpenseTypeId { get; set; }

        [DisplayName("Expense Type")]
        public string? ExpenseTypeName { get; set; }
    }
}
