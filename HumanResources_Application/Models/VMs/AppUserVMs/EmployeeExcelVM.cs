using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Models.VMs.AppUserVMs
{
    public class EmployeeExcelVM
    {
        [DisplayName("Full Name")]
        public string FullName { get; set; }

        [DisplayName("E-Mail")]
        public string? Email { get; set; }

        [DisplayName("Identity Number")]
        public string? IdentityNumber { get; set; }

        [DisplayName("Department Name")]
        public string? DepartmanName { get; set; }

        public string? Title { get; set; }

        [DisplayName("Phone Number")]
        public string? PhoneNumber { get; set; }

        [DisplayName("Start Work Date")]
        public string StartWorkDate { get; set; }

    }
}
