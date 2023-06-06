using HumanResources_Domain.Entities;
using HumanResources_Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Models.VMs.AdvanceVMs
{
    public class AdvanceVM
    {
        public int? Id { get; set; }

        public decimal? AdvanceAmount { get; set; }

        public ApproveStatus? ApproveStatus { get; set; }

        public string CreateDate { get; set; }

        [DisplayName("Payment Due Date")]
        public string PaymentDueDate { get; set; }

        public DateTime? UpdateDate { get; set; }
        public Status? Status { get; set; }
        public string? FullName { get; set; }

        public string? Explanation { get; set; }

        public Guid? ExecutiveId { get; set; }

        [DisplayName("Executive Name")]
        public string? ExecutiveName { get; set; }

        public Guid? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }


      

    }
}
