using HumanResources_Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Infrastructure.EntityTypeConfig
{
    internal class CompanyConfig : BaseEntityConfig<Company>
    {
        public override void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CompanyName).IsRequired(true).HasMaxLength(30);
            builder.Property(x => x.CompanyEmail).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.CompanyPhoneNumber).IsRequired(true);
            builder.Property(x => x.NumberOfEmployees).IsRequired(true);
            builder.Property(x => x.TaxNo).IsRequired(false);
            builder.Property(x => x.TaxOffice).IsRequired(false);
            builder.Property(x => x.City).IsRequired(false);
            builder.Property(x => x.Country).IsRequired(false);
            builder.Property(x => x.Address).IsRequired(false);
            builder.Property(x => x.PostCode).IsRequired(false);


           
            builder.HasMany(x => x.CompanyManagers)
                .WithOne(x => x.Company)
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);
               


            base.Configure(builder);
        }
    }
}
