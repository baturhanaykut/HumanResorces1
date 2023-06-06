using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HumanResources_Application.Models.VMs.LeaveTypeVMs
{
    public class LeaveTypeVM
    {
        public int Id { get; set; }

        [Display(Name = "Leave Type Name")]
        public string LeaveTypeName { get; set; }

        [Display(Name = "Leave Default Days")]
        public decimal? DefaultDays { get; set; }
    }
}
