using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Models.DTOs.AppUserDTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email cannot be null")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password cannot be null")]
        [MinLength(4, ErrorMessage = "Password must be more than 4 characters.")]
        [MaxLength(20, ErrorMessage = "Password must be less than 20 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [ValidateNever]
        public IdentityResult Result { get; set; }

    }
}
