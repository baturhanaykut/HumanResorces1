using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HumanResources_Domain.Enums
{
    public enum ApproveStatus
    {
        Approved = 1,
        Denied =2,
        [Display(Name = "In Approval")]
        InApproval = 3
    }
}
