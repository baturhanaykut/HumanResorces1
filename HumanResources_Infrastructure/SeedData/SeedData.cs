using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HumanResources_Domain.Entities;
using HumanResources_Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using HumanResources_Infrastructure.EntityTypeConfig;

namespace HumanResources_Infrastructure.SeedData
{
    public static class SeedData
    {
        public async static Task Seed(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateAsyncScope())
            {
                AppDbContext context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.Migrate();

                if (!context.Roles.Any())
                {
                    var roleStore = new RoleStore<AppRole, AppDbContext, Guid>(context);

                    await roleStore.CreateAsync(new AppRole() { Name = "SiteManager", NormalizedName = "SiteManager", Status = Status.Active });
                    await roleStore.CreateAsync(new AppRole() { Name = "CompanyManager", NormalizedName = "CompanyManager", Status = Status.Active });
                    await roleStore.CreateAsync(new AppRole() { Name = "Employee", NormalizedName = "Employee", Status = Status.Active });
                    await context.SaveChangesAsync();
                }

                if (!context.Users.Any())
                {
                    Company company = new Company
                    {
                        Id = Guid.NewGuid(),
                        CompanyName = "Human Resorces",
                        CompanyPhoneNumber = "02165081919",
                        NumberOfEmployees = 50,
                        CompanyEmail = "staffingadvantage.hr@gmail.com",
                        CreateDate = DateTime.Now,
                        Status = Status.Active,
                    };

                    await context.Companies.AddAsync(company);

                    var passwordHasher = new PasswordHasher<AppUser>();

                    //MANAGER:
                    AppUser companyManagerUser = new AppUser
                    {

                        UserName = "companymanager@hr.com",
                        NormalizedUserName = "COMPANYMANAGER@HR.COM",
                        FirstName = "Company",
                        LastName = "Manager",
                        IdentityNumber = "11111111112",
                        Email = "companymanager@hr.com",
                        NormalizedEmail = "COMPANYMANAGER@HR.COM",
                        ImagePath = "/images/noImage.png",
                        SecurityStamp = Guid.NewGuid().ToString(),
                        Status = Status.Active,
                        CreateDate = DateTime.Now,
                        StartWorkDate = DateTime.Now,
                        BirthDate = DateTime.Now,
                        CompanyId = company.Id,
                        EmailConfirmed = true,
                        ExecutiveStatus = false

                    };


                    var hashedCompanyManager = passwordHasher.HashPassword(companyManagerUser, "1234");
                    companyManagerUser.PasswordHash = hashedCompanyManager;

                    var userStore = new UserStore<AppUser, AppRole, AppDbContext, Guid>(context);

                    try
                    {
                        await userStore.CreateAsync(companyManagerUser);
                        await userStore.AddToRoleAsync(companyManagerUser, "CompanyManager");
                    }
                    catch (Exception ex)
                    {
                        var Error = ex.Message;
                        throw;
                    }





                    await context.SaveChangesAsync();


                    //EMPLOYEE:
                    AppUser employeeUser = new AppUser
                    {
                        UserName = "employee@hr.com",
                        NormalizedUserName = "EMPLOYEE@HR.COM",
                        FirstName = "Employee",
                        LastName = "Employee",
                        IdentityNumber = "11111111113",
                        Email = "employee@hr.com",
                        NormalizedEmail = "EMPLOYEE@HR.COM",
                        ImagePath = "/images/noImage.png",
                        SecurityStamp = Guid.NewGuid().ToString(),
                        Status = Status.Active,
                        CreateDate = DateTime.Now,
                        StartWorkDate = DateTime.Now,
                        BirthDate = DateTime.Now,
                        CompanyId = company.Id,
                        ExecutiveId = companyManagerUser.ExecutiveId,
                        EmailConfirmed = true,
                        ExecutiveStatus = false
                    };

                    var hashedEmployee = passwordHasher.HashPassword(employeeUser, "1234");
                    employeeUser.PasswordHash = hashedEmployee;

                    var employeStore = new UserStore<AppUser, AppRole, AppDbContext, Guid>(context);
                    try
                    {
                        await employeStore.CreateAsync(employeeUser);
                        await employeStore.AddToRoleAsync(employeeUser, "Employee");

                    }
                    catch (Exception ex)
                    {
                        var Error = ex.Message;

                    }



                    AppUser siteManagerUser = new AppUser
                    {
                        UserName = "staffingadvantage.hr@gmail.com",
                        NormalizedUserName = "STAFFINGADVANTAGE.HR@GMAIL.COM",
                        FirstName = "Site",
                        LastName = "Manager",
                        IdentityNumber = "11111111114",
                        Email = "staffingadvantage.hr@gmail.com",
                        NormalizedEmail = "STAFFINGADVANTAGE.HR@GMAIL.COM",
                        ImagePath = "/images/noImage.png",
                        SecurityStamp = Guid.NewGuid().ToString(),
                        Status = Status.Active,
                        CreateDate = DateTime.Now,
                        StartWorkDate = DateTime.Now,
                        BirthDate = DateTime.Now,
                        EmailConfirmed=true,
                        ExecutiveStatus = false

                    };

                    var hashedSiteManager = passwordHasher.HashPassword(siteManagerUser, "1234");
                    siteManagerUser.PasswordHash = hashedSiteManager;

                    var siteStore = new UserStore<AppUser, AppRole, AppDbContext, Guid>(context);
                    await siteStore.CreateAsync(siteManagerUser);
                    await siteStore.AddToRoleAsync(siteManagerUser, "SiteManager");

                    await context.SaveChangesAsync();
                }

            }
        }
    }
}