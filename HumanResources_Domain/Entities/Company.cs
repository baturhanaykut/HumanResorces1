using HumanResources_Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Domain.Entities
{
    public class Company : IBaseEntity
    {
        public Company()
        {
            CompanyManagers = new HashSet<AppUser>();
            ExpenseTypes = new HashSet<ExpenseType>();
            LeaveTypes = new HashSet<LeaveType>();
        }
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyPhoneNumber { get; set; }
        public string TaxNo { get; set; }
        public string TaxOffice { get; set; }
        public int NumberOfEmployees { get; set; }
        public string CompanyEmail { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

        //Navigation Property

        public virtual ICollection<AppUser> CompanyManagers { get; set; }
        public virtual ICollection<ExpenseType> ExpenseTypes { get; set; }
        public virtual ICollection<LeaveType> LeaveTypes { get; set; }

    }
}
