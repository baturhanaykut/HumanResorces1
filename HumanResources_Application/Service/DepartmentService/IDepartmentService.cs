using HumanResources_Application.Models.VMs.AddressVMs;
using HumanResources_Application.Models.VMs.DepartmentVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Service.DepartmentService
{
    public interface IDepartmentService
    {
        Task<List<DepartmentVM>> GetDepartments();
    }
}
