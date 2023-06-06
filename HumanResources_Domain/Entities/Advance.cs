using HumanResources_Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Domain.Entities
{
    public class Advance : IBaseEntity
    {
        public int Id { get; set; }
        public string Explanation { get; set; }
        public decimal AdvanceAmount { get; set; }
        public ApproveStatus ApproveStatus { get; set; }
        public DateTime PaymentDueDate { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

        //Navigation Properties

        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }



    }
}
