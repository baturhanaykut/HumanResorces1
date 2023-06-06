using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HumanResources_Domain.Enums
{
    public enum Status
    {
        [Display(Name = "Active")]
        Active = 1,
        //[Display(Name = "Active-Modified")]
        //Modified = 2,
        [Display(Name = "Passive")]
        Passive = 2,
        [Display(Name = "Awating Approval")]
        AwatingApproval = 3,
    }
}
