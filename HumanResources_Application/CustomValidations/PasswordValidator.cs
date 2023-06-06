using HumanResources_Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.CustomValidations
{
    //NOT: Password Validasyonu program.cs AddIdentity metoduna eklenir.
    //TO DO: Vaidasyon geliştirilebilir.

    public class PasswordValidator : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
        {
            var errors = new List<IdentityError>();

            if (password!.ToLower().Contains(user.FirstName!.ToLower()))
            {
                errors.Add(new() { Code = "PasswordNoCoontainUserName", Description = "The password field cannot contain a first name." });
            }

            if (password!.ToLower().StartsWith("1234"))
            {
                errors.Add(new() { Code = "PasswordCoontain1234", Description = "Password cannot contain consecutive numbers." });
            }

            if (errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }

            return Task.FromResult(IdentityResult.Success);
        }
    }
}
