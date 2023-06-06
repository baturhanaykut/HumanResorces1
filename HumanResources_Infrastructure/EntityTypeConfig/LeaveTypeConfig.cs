using HumanResources_Domain.Entities;
using HumanResources_Domain.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Infrastructure.EntityTypeConfig
{
    internal class LeaveTypeConfig : BaseEntityConfig<LeaveType>
    {
        public override void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.LeaveTypeName)
               .IsRequired(true)
               .HasMaxLength(50);

            builder.Property(x => x.DefaultDays)
               .IsRequired(true);

            builder.HasData(
                        new LeaveType() { Id = 1, LeaveTypeName = "Annual Leave", Status = Status.Active, CreateDate = DateTime.Now, DefaultDays = 14 },
                        new LeaveType() { Id = 2, LeaveTypeName = "Maternity Leave", Status = Status.Active, CreateDate = DateTime.Now, DefaultDays = 180 },
                        new LeaveType() { Id = 3, LeaveTypeName = "Paternity Leave", Status = Status.Active, CreateDate = DateTime.Now, DefaultDays = 14 }

             );

            base.Configure(builder);
        }
    }
}

