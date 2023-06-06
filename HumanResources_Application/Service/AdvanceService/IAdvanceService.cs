using HumanResources_Application.Models.DTOs.AdvanceDTO;
using HumanResources_Application.Models.VMs.AdvanceVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Service.AdvanceService
{
    public interface IAdvanceService
    {
        
        Task<bool> Create(CreateAdvanceDTO model, string userName);  
        Task Update(UpdateAdvanceDTO model);
        Task Delete(int id);      
        Task<UpdateAdvanceDTO> GetByID(int id);      
        Task<List<AdvanceVM>> GetAdvances(Guid? id);
        Task<List<AdvanceVM>> GetAdvancesCompanyManager(Guid? id);
        Task<List<AdvanceVM>> GetPersonelAdvances(string userName);
        Task<AdvanceVM> GetAdvanceDetails(int id);
        Task Accept(int id);
        Task Reject(int id);
        Task<List<AdvanceVM>> GetAdvancesCompanyManagerAwatingApproval(Guid? id);

        Task<List<AdvanceVM>> GetAdvancesManagerAwatingApproval(Guid? id);
    }
}
