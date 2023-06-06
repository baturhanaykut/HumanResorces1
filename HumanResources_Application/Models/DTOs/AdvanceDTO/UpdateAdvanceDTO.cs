using HumanResources_Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Models.DTOs.AdvanceDTO
{
    public class UpdateAdvanceDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Explanation area cannot be empty!")]
        [MaxLength(50)]
        public string Explanation { get; set; }


        [Required(ErrorMessage = "Amount area cannot be empty!")]
        [Range(0, 99999.99, ErrorMessage = "Please input between 0-99999.99!")]
        public decimal AdvanceAmount { get; set; }

        [Required(ErrorMessage = "Payment date cannot be blank!"), DataType(DataType.DateTime)]
        public DateTime PaymentDueDate { get; set; }

        public DateTime UpdateDate => DateTime.Now;

        
        public DateTime CreateDate { get; set; }

        public Status Status => Status.Active; //Modified'tan aktife çekildi.

        public ApproveStatus ApproveStatus { get; set; }

        public Guid? ExecutiveId { get; set; }

        public Guid AppUserId { get; set; }
    }
}
