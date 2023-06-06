using HumanResources_Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Models.VMs.CompanyVMS
{
    public class CompanyVM
    {
        public Guid Id { get; set; }
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }

        [DisplayName("Company Email")]
        public string CompanyEmail { get; set; }

        [DisplayName("Company Phone Number")]
        public string CompanyPhoneNumber { get; set; }

        [DisplayName("Number Of Employees")]
        public int NumberOfEmployees { get; set; }

        [DisplayName("Tax No")]
        public string TaxNo { get; set; }

        [DisplayName("Tax Office")]
        public string TaxOffice { get; set; }

        [DisplayName("Split Date")]
        public string CreateDate { get; set; }

        public string City { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }

        public Status Status { get; set; }

    }
}
