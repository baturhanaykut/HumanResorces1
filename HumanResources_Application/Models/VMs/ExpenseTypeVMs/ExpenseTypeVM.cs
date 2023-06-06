using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HumanResources_Application.Models.VMs.ExpenseTypeVMs
{
    public class ExpenseTypeVM
    {
        public int? Id { get; set; }

        [Display(Name = "Expense Type")]
        public string? ExpenseTypeName { get; set; }
    }
}
