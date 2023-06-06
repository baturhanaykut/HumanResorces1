using HumanResources_Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Models.DTOs.AppUserDTOs
{
    public class RegisterDTO
    {
        //Company Administrator
        [Required(ErrorMessage = "First Name cannot be null.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name cannot be null.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Identity Number cannot be null.")] //Numerik yap.
        public string IdentityNumber { get; set; }

        [EmailAddress(ErrorMessage = "The email format is incorrect.")]
        [Required(ErrorMessage = "Email cannot be null.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password cannot be null.")]
        [MinLength(4, ErrorMessage = "Password must be more than 4 characters.")]
        [MaxLength(20, ErrorMessage = "Password must be less than 20 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "The entered passwords are not the same.")]
        [Required(ErrorMessage = "Confirm Password cannot be null.")]
        public string ConfirmPassword { get; set; }

        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Passive;

        //Company 

        public string CompanyName  { get; set; }

        public string CompanyPhoneNumber { get; set; }

        public string CompanyEmail { get; set; }

        public int NumberOfEmployees { get; set; }

        public DateTime CompanyCreateDateTime => DateTime.Now;

        public Status CompanyStatus => Status.Passive;

    }
}
