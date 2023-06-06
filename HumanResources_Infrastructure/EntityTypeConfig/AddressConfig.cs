using HumanResources_Domain.Entities;
using HumanResources_Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Infrastructure.EntityTypeConfig
{
    internal class AddressConfig : BaseEntityConfig<Address>
    {
        public override void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(x => x.AddressId);

            builder.Property(x => x.AddressId)
                .HasColumnOrder(1);

            builder.Property(x => x.Description)
                .IsRequired(true)
                .IsUnicode(true)
                .HasColumnType("NVARCHAR(MAX)")
                .HasColumnOrder(2);

            builder.Property(x => x.PostCode)
                .IsRequired(false)
                .HasColumnOrder(3);

            builder.Property(x => x.DistrictId)
                .IsRequired(true)
                .HasColumnOrder(4);

            builder.Property(x => x.AppUserId)
                .IsRequired(true)
                .HasColumnOrder(5);

            builder.HasOne(x=>x.District)
                .WithMany(x=>x.Adresses)
                .HasForeignKey(x=>x.DistrictId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne<AppUser>(x => x.AppUser)
                .WithOne(x => x.Address)
                .HasForeignKey<Address>(x => x.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);


            base.Configure(builder);
        }
    }
}
