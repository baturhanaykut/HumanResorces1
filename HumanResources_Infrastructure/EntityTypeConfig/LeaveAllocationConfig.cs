using HumanResources_Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Infrastructure.EntityTypeConfig
{
    internal class LeaveAllocationConfig : BaseEntityConfig<LeaveAllocation>
    {
        public override void Configure(EntityTypeBuilder<LeaveAllocation> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.NumberOfDays)
               .IsRequired(true);

            builder.Property(x => x.Period)
               .IsRequired(true);



            base.Configure(builder);
        }
    }
}
