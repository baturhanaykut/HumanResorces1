using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Models.VMs.AppUserVMs
{
    public class ResetPasswordVM
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
