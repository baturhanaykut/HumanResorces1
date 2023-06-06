using HumanResources_Domain.Entities;
using HumanResources_Infrastructure.EntityTypeConfig;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Infrastructure
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        //DbSet
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Advance> Advances { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AppUserConfig());
            builder.ApplyConfiguration(new LeaveRequestConfig());
            builder.ApplyConfiguration(new LeaveTypeConfig());
            builder.ApplyConfiguration(new LeaveAllocationConfig());
            builder.ApplyConfiguration(new DepartmentConfig());
            builder.ApplyConfiguration(new AdvanceConfig());
            builder.ApplyConfiguration(new CityConfig());
            builder.ApplyConfiguration(new DistrictConfig());
            builder.ApplyConfiguration(new AddressConfig());
            builder.ApplyConfiguration(new CompanyConfig());
            builder.ApplyConfiguration(new ExpenseConfig());
            builder.ApplyConfiguration(new ExpenseTypeConfig());

            base.OnModelCreating(builder);
        }
    }
}
