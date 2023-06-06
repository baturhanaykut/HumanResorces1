using HumanResources_Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Models.DTOs.CompanyDTOs
{
    public class CompanyUpdateDTO
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }

        public string CompanyPhoneNumber { get; set; }

        public string CompanyEmail { get; set; }

        public int NumberOfEmployees { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Address { get; set; }

        public string TaxNo { get; set; }

        public string TaxOffice { get; set; }
        public string PostCode { get; set; }

        public DateTime CompanyUpdateDateTime => DateTime.Now;

        public DateTime CreateDate { get; set; }

        public Status CompanyStatus => Status.Active; //Modified'tan aktife çekildi.

    }
}
