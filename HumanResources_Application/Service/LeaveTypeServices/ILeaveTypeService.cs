using HumanResources_Application.Models.DTOs.LeaveTypeDTOs;
using HumanResources_Application.Models.VMs.LeaveTypeVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Service.LeaveTypeServices
{
    public interface ILeaveTypeService
    {
        Task<bool> Create(CreateLeaveTypeDTO model, string userName);

        Task Update(UpdateLeaveTypeDTO model, string userName);

        Task Delete(int id);

        Task<List<LeaveTypeVM>> GetLeaveTypes(Guid? id);

        Task<UpdateLeaveTypeDTO> GetById(int id);
    }
}
