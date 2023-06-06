using HumanResources_Application.Models.DTOs.ExpenseTypeDTOs;
using HumanResources_Application.Models.VMs.ExpenseTypeVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Service.ExpenseTypeServices
{
    public interface IExpenseTypeService
    {
        Task<bool> Create(CreateExpenseTypeDTO model, string userName);

        Task Update(UpdateExpenseTypeDTO model, string userName);

        Task Delete(int id);

        Task<List<ExpenseTypeVM>> GetExpenseTypes(Guid? id);

        Task<UpdateExpenseTypeDTO> GetById(int id);
    }
}
