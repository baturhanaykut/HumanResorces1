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
    internal class DistrictConfig : BaseEntityConfig<District>
    {
        public override void Configure(EntityTypeBuilder<District> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.DistrictName).IsRequired(false);

            builder.HasData(
                        new District() { Id = 1, DistrictName = "Kadıköy", Status = Status.Active, CreateDate = DateTime.Now, CityId = 1 },
                        new District() { Id = 2, DistrictName = "Kızlay", Status = Status.Active, CreateDate = DateTime.Now, CityId = 2 },
                        new District() { Id = 3, DistrictName = "Merkez", Status = Status.Active, CreateDate = DateTime.Now, CityId = 3 },
                        new District() { Id = 4, DistrictName = "Namık Kemal", Status = Status.Active, CreateDate = DateTime.Now, CityId = 4 },
                        new District() { Id = 5, DistrictName = "Konya Altı", Status = Status.Active, CreateDate = DateTime.Now, CityId = 5 },
                        new District() { Id = 6, DistrictName = "Alsancak", Status = Status.Active, CreateDate = DateTime.Now, CityId = 6 }
           );

            base.Configure(builder);
        }
    }
}
