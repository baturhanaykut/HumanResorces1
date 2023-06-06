using HumanResources_Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Domain.Entities
{
    public class Department : IBaseEntity
    {
        public Department()
        {
            AppUsers = new HashSet<AppUser>();
        }

        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }


        public int? Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<AppUser> AppUsers { get; set; }
    }
}
