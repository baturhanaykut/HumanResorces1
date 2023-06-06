using HumanResources_Domain.Entities;
using Microsoft.AspNetCore.Http;
using HumanResources_Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace HumanResources_Application.Models.VMs.AppUserVMs
{
    public class EmployeeVM
    {
        public Guid? Id { get; set; }
        public string UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [DisplayName("E-Mail")]
        public string? Email { get; set; }
        [DisplayName("Identity Number")]
        public string? IdentityNumber { get; set; }
        public BloodGroup? BloodGroup { get; set; }

        [DisplayName("Phone Number")]
        public string? PhoneNumber { get; set; }

        public Gender? Gender { get; set; }
        public Status? Status { get; set; }
        public string? AddressDescription { get; set; }
        public int? CityId { get; set; }
        public string? CityName { get; set; }
        public int? DistrictId { get; set; }

        [DisplayName("Postal Code")]
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? Title { get; set; }
        public int? DepartmanId { get; set; }
        [DisplayName("Department Name")] 
        public string? DepartmanName { get; set; }
        public string? Job { get; set; }
        public string? ImagePath { get; set; }
        [DisplayName("Start Work Date")]
        public string? StartWorkDate { get; set; }

        [DisplayName("Birth Date")]
        public string BirthDate { get; set; }
        [DisplayName("Full Name")]
        public string FullName { get; set; }

        public Guid? ExecutiveId { get; set; }

        public bool? ExecutiveStatus { get; set; }

        [DisplayName ("Executive Name")]
        public string? ExecutiveName { get; set; }

        [DisplayName("Company Id")]
        public Guid? CompanyId { get; set; }

        //Navigation Property
        public Department? Departman { get; set; }
        public virtual ICollection<LeaveRequest>? Leaves { get; set; }

        public string? Role { get; set; }
    }
}
