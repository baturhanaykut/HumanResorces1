using HumanResources_Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Infrastructure.EntityTypeConfig
{
    internal class AppUserConfig : BaseEntityConfig<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName).
                IsRequired(true).
                HasMaxLength(30);

            builder.Property(x => x.LastName).
                IsRequired(true).
                HasMaxLength(30);

            builder.Property(x => x.IdentityNumber).
                IsRequired(true).
                HasColumnType("char").
                HasMaxLength(11);

            builder.Property(x => x.ExecutiveId)
               .IsRequired(false);

            builder.HasOne(x => x.Executive)
                   .WithMany(x => x.Employees)
                   .HasForeignKey(x => x.ExecutiveId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Department)
                .WithMany(x => x.AppUsers)
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

           

            base.Configure(builder);
        }
    }
}
