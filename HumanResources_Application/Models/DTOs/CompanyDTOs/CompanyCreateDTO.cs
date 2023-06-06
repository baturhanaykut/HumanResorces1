using HumanResources_Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Models.DTOs.CompanyDTOs
{
    public class CompanyCreateDTO
    {
        public string CompanyName { get; set; }

        public string CompanyPhoneNumber { get; set; }

        public string CompanyEmail { get; set; }

        public int NumberOfEmployees { get; set; }

        public DateTime CompanyCreateDateTime => DateTime.Now;

        public Status CompanyStatus => Status.Active;
    }
}
