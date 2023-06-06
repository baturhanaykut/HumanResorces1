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
    internal class LeaveRequestConfig : BaseEntityConfig<LeaveRequest>
    {
        public override void Configure(EntityTypeBuilder<LeaveRequest> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.StartDate)
               .IsRequired(true)
               .HasColumnType("datetime2");

            builder.Property(x => x.EndDate)
               .IsRequired(true)
               .HasColumnType("datetime2");

            builder.Property(x => x.LeaveDay)
                .IsRequired(true);


            builder.Property(x => x.RequestComments)
               .IsRequired(true)
               .HasMaxLength(100);

            builder.HasOne(x => x.LeaveType)
                  .WithMany(x => x.LeaveRequests)
                  .HasForeignKey(x => x.LeaveTypeId)
                  .OnDelete(DeleteBehavior.Restrict);

            base.Configure(builder);
        }
    }
}
