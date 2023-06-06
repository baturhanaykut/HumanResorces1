using HumanResources_Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Domain.Entities
{
    public class AppUser : IdentityUser<Guid>, IBaseEntity
    {
        public AppUser()
        {
            Leaves = new HashSet<LeaveRequest>();
            Advances = new HashSet<Advance>();
            Employees = new HashSet<AppUser>();
            Expenses = new HashSet<Expense>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Title { get; set; }
        public string? Job { get; set; }
        public DateTime StartWorkDate { get; set; }
        public int? DepartmentId { get; set; }
        public string? ImagePath { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }
        public BloodGroup? BloodGroup { get; set; }
        public Gender? Gender { get; set; }

        public Guid? ExecutiveId { get; set; } // Yönetici Eklemek İçin yazıldı.

        public bool ExecutiveStatus { get; set; }

        public Guid? CompanyId { get; set; }

        [NotMapped]
        public IFormFile UploadPath { get; set; }

        [NotMapped]       
        public string FullName => FirstName + " " + LastName;

        //Navigation Property

        public Department? Department { get; set; }
        public Address Address { get; set; }
        public virtual AppUser? Executive { get; set; }
        public Company? Company { get; set; }

        public virtual ICollection<AppUser> Employees { get; set; }
        public virtual ICollection<LeaveRequest> Leaves { get; set; }
        public virtual ICollection<Advance> Advances { get; set; }
        public virtual ICollection<Expense> Expenses { get; set; }

    }
}
