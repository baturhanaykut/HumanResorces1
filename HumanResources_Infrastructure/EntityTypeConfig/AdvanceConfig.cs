using HumanResources_Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Infrastructure.EntityTypeConfig
{
    internal class AdvanceConfig:BaseEntityConfig<Advance>
    {
        public override void Configure(EntityTypeBuilder<Advance> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Explanation)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.AdvanceAmount)
                .IsRequired();

            builder.HasOne(x => x.AppUser)
                  .WithMany(x => x.Advances)
                  .HasForeignKey(x => x.AppUserId)
                  .OnDelete(DeleteBehavior.Restrict);

            base.Configure(builder);

        }
    }
}
