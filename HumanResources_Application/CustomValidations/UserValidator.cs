using HumanResources_Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.CustomValidations
{
    //NOT: İsim-Soyisim Validasyonu program.cs AddIdentity metoduna eklenir.

    //TO DO: Tüm isimde kontrol sağlanamıyor, tekrar bakılacak.

    //public class UserValidator : IUserValidator<AppUser>
    //{
    //    int i;

    //    public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
    //    {
    //        var errors = new List<IdentityError>();
    //        var isNumberFirstName = int.TryParse(user.FirstName[i]!.ToString(), out _);
    //        var isNumberLastName = int.TryParse(user.LastName[i]!.ToString(), out _);


    //        for (i = 0; i < user.FirstName.Length; i++)
    //        {
    //            if (isNumberFirstName)
    //            {
    //                errors.Add(new() { Code = "FirstNameContainNumber", Description = "The first name cannot contain numbers." });
    //            }
    //        }


    //        for (i = 0; i < user.LastName.Length; i++)
    //        {
    //            if (isNumberLastName)
    //            {
    //                errors.Add(new() { Code = "LastNameContainNumber", Description = "The last name cannot contain numbers." });
    //            }
    //        }

    //        if (errors.Any())
    //        {
    //            return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
    //        }

    //        return Task.FromResult(IdentityResult.Success);
    //    }
    //}
}
