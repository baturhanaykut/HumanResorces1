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

            base.Configure(builder);
        }
    }
}

