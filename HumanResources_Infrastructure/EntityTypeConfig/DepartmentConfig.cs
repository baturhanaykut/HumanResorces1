using HumanResources_Domain.Entities;
using HumanResources_Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace HumanResources_Infrastructure.EntityTypeConfig
{
    internal class DepartmentConfig : BaseEntityConfig<Department>
    {
        public override void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Department");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnOrder(1);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(true)
                .HasColumnOrder(2);

            builder.HasIndex(x => x.Name)
                .IsUnique(true);

            builder.HasData(
                         new Department() { Id = 1, Name = "Human Resources", Status = Status.Active, CreateDate = DateTime.Now },
                         new Department() { Id = 2, Name = "Information Technologies", Status = Status.Active, CreateDate = DateTime.Now },
                         new Department() { Id = 3, Name = "Quality Assurance", Status = Status.Active, CreateDate = DateTime.Now },
                         new Department() { Id = 4, Name = "Accounting", Status = Status.Active, CreateDate = DateTime.Now },
                         new Department() { Id = 5, Name = "Quality Control", Status = Status.Active, CreateDate = DateTime.Now },
                         new Department() { Id = 6, Name = "Engineering", Status = Status.Active, CreateDate = DateTime.Now }
            );


            base.Configure(builder);
        }
    }
}
