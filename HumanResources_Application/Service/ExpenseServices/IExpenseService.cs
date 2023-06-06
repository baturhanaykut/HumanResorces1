using HumanResources_Application.Models.DTOs.ExpenseDTOs;
using HumanResources_Application.Models.VMs.ExpenseVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Service.ExpenseServices
{
    public interface IExpenseService
    {
        Task<bool> Create(CreateExpenseDTO model, string userName);

        Task Update(UpdateExpenseDTO model);

        Task Delete(int id);

        Task<UpdateExpenseDTO> GetById(int id);

        Task<List<ExpenseVM>> GetExpenses(Guid? id);

        Task<List<ExpenseVM>> GetPersonelExpenses(string userName);

        Task<List<ExpenseVM>> GetExpensesEmployee(Guid? id);

        Task Accept(int id);
        Task Reject(int id);

        Task<List<ExpenseVM>> GetExpensesCompanyManagerAwatingApproval(Guid? id);

        Task<List<ExpenseVM>> GetExpensesManagerAwatingApproval(Guid? id);
    }
}
