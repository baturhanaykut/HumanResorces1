using HumanResources_Application.Models.VMs.AdvanceVMs;
using HumanResources_Application.Models.VMs.AppUserVMs;
using HumanResources_Application.Models.VMs.ExpenseVMs;
using HumanResources_Application.Models.VMs.LeaveVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace HumanResources_Application.Models.VMs
{
    public class ViewModel
    {
        public IPagedList<EmployeeVM> Employee { get; set; }
        public List<AdvanceVM> Advance { get; set; }
        public List<ExpenseVM> Expense { get; set; }
        public List<LeaveVM> Leave { get; set; }
   
    }
}
