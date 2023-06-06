using HumanResources_Application.Models.VMs.AppUserVMs;
using HumanResources_Application.Models.VMs.DepartmentVMs;
using HumanResources_Domain.Entities;
using HumanResources_Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Models.DTOs.AppUserDTOs
{
    public class CreateEmployeeDTO
    {

        [Required(ErrorMessage = "First Name cannot be null.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name cannot be null.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Identity Number cannot be null.")] //Numerik yap.
        public string IdentityNumber { get; set; }

        [EmailAddress(ErrorMessage = "The email format is incorrect.")]
        [Required(ErrorMessage = "Email cannot be null.")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Password cannot be null.")]
        //[MinLength(4, ErrorMessage = "Password must be more than 4 characters.")]
        //[MaxLength(20, ErrorMessage = "Password must be less than 20 characters.")]
        //[DataType(DataType.Password)]
        public string? Password { get; set; }

        //[Compare(nameof(Password), ErrorMessage = "The entered passwords are not the same.")]
        //[Required(ErrorMessage = "Confirm Password cannot be null.")]
        public string? ConfirmPassword { get; set; }

        public BloodGroup BloodGroup { get; set; }

        public string PhoneNumber { get; set; }

        public Gender Gender { get; set; }

        public DateTime? BirthDate { get; set; }


        public string? AddressDescription { get; set; }

        [Display(Name = ("City"))]
        public int? CityId { get; set; }

        [Display(Name = ("District"))]
        public int? DistrictId { get; set; }


        public DateTime? StartWorkDate { get; set; }
        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;

        public string? Job { get; set; }

        public string? Title { get; set; }

        public string? ImagePath { get; set; }

        public int? DepartmentId { get; set; }

        [Display(Name = ("Executive"))]
        public Guid? ExecutiveId { get; set; } // Yönetici Eklemek İçin yazıldı.

        [Display(Name = ("Executive Status"))]
        public bool ExecutiveStatus { get; set; }

        public string Role { get; set; }

        public virtual ICollection<EmployeeVM>? Employees { get; set; }

        public virtual ICollection<DepartmentVM>? Departments { get; set; }
    }
}
