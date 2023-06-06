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
    internal class ExpenseTypeConfig : BaseEntityConfig<ExpenseType>
    {
        public override void Configure(EntityTypeBuilder<ExpenseType> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ExpenseTypeName)
               .IsRequired(true)
               .HasMaxLength(50);

            base.Configure(builder);
        }
    }
}
