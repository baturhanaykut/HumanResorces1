using HumanResources_Application.Extensions;
using HumanResources_Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Models.DTOs.AppUserDTOs
{
    public class UpdateProfileDTO
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }

        //General Informtaion

        [MinLength(2, ErrorMessage = "First name must be more than 6 characters.")]
        [MaxLength(30, ErrorMessage = "First name must be less than 30 characters.")]
        [Required(ErrorMessage = "Fist name cannot be null.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Last Name cannot be null")]
        [Display(Name = "First Name")]
        public string? LastName { get; set; }


        [Required(ErrorMessage = "Identity Number cannot be null")]
        [Display(Name = "Identity Number")]
        public string? IdentityNumber { get; set; }

        [Display(Name = "Title")]
        public string? Title { get; set; }

        [Display(Name = "Job")]
        public string? Job { get; set; }

        [Display(Name = "Gender Type")]
        public Gender? Gender { get; set; }

        [Display(Name = "Blood Group")]
        [ValidateNever]
        public BloodGroup? BloodGroup { get; set; }


        //Contact Information 
        [Display(Name = "Phone Number")]
       
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email cannot be null")]
        public string? Email { get; set; }

        public string? Password { get; set; }
        
        public string? ConfirmPassword { get; set; }

        [DisplayName("Start Work Date")]
        public DateTime StartWorkDate { get; set; }

        [Display(Name = ("City"))]
        public int? CityId { get; set; }

        public string? CityName { get; set; }

        [Display(Name = ("District"))]
        public int? DistrictId { get; set; }
        public string? DistrictName { get; set; }
        public string? AddressDescription { get; set; }
        
        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }

        public string? DepartmentName { get; set; }

        public DateTime UpdateDate => DateTime.Now;
        public Status Status => Status.Active; //Modified'tan aktife çekildi.

        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        [Display(Name = ("Executive Name"))]
        public Guid? ExecutiveId { get; set; }

        [Display(Name = ("Executive Status"))]
        public bool ExecutiveStatus { get; set; }

        [DisplayName("Executive Name")]
        public string? ExecutiveName { get; set; }

        [ValidateNever]
        public string ImagePath { get; set; }

        [ValidateNever]
        [PictureFileExtention]
        public IFormFile? UploadPath { get; set; }

        [ValidateNever]
        public string FullName { get; set; }

        public string? Role { get; set; }
    }
}
