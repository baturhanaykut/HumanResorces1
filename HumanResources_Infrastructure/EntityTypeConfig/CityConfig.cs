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
    internal class CityConfig : BaseEntityConfig<City>
    {
        public override void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Districts)
                .WithOne(x => x.City)
                .HasForeignKey(x => x.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.CityName).IsRequired(false);

            builder.HasData(
                         new City() { Id = 1, CityName = "İstanbul", Status = Status.Active, CreateDate = DateTime.Now },
                         new City() { Id = 2, CityName = "Ankara", Status = Status.Active, CreateDate = DateTime.Now },
                         new City() { Id = 3, CityName = "Bursa", Status = Status.Active, CreateDate = DateTime.Now },
                         new City() { Id = 4, CityName = "Tekirdağ", Status = Status.Active, CreateDate = DateTime.Now },
                         new City() { Id = 5, CityName = "Antalya", Status = Status.Active, CreateDate = DateTime.Now },
                         new City() { Id = 6, CityName = "İzmir", Status = Status.Active, CreateDate = DateTime.Now }
            );

            base.Configure(builder);
        }
    }
}
