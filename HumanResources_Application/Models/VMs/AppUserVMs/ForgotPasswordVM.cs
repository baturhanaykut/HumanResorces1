using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Models.VMs.AppUserVMs
{
    public class ForgotPasswordVM
    {
        [ValidateNever]
        public IdentityResult Result{ get; set; }
        [ValidateNever]
        public string Token { get; set; }
        public string Email { get; set; }
    }
}
