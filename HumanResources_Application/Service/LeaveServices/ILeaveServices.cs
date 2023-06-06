using HumanResources_Application.Models.DTOs.LeaveDTOs;
using HumanResources_Application.Models.VMs.LeaveTypeVMs;
using HumanResources_Application.Models.VMs.LeaveVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Service.LeaveServices
{
    public interface ILeaveService
    {
        Task<bool> Create(CreateLeaveDTO model, string userName);
        Task Update(UpdateLeaveDTO model);
        Task Delete(int id);

        Task<UpdateLeaveDTO> GetByID(int id);
        Task<List<LeaveVM>> GetLeaves(Guid? id);
        Task<List<LeaveVM>> GetPersonelLeaves(string userName);

        Task<List<LeaveTypeVM>> GetLeaveTypes();

        Task<List<LeaveVM>> GetLeaveEmployee(Guid? id);

        Task Accept(int id);
        Task Reject(int id);

        Task<List<LeaveVM>> GetLeavesCompanyManagerAwatingApproval(Guid? id);

        Task<List<LeaveVM>> GetLeavesManagerAwatingApproval(Guid? id);
    }
}
