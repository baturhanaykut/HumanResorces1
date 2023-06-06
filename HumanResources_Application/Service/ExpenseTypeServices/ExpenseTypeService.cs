using AutoMapper;
using HumanResources_Application.Models.DTOs.ExpenseTypeDTOs;
using HumanResources_Application.Models.VMs.ExpenseTypeVMs;
using HumanResources_Application.Service.CompanyServices;
using HumanResources_Domain.Entities;
using HumanResources_Domain.Enums;
using HumanResources_Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Service.ExpenseTypeServices
{
    public class ExpenseTypeService : IExpenseTypeService
    {
        private readonly IExpenseTypeRepository _expenseTypeRepository;
        private readonly IMapper _mapper;
        private readonly ICompanyService _companyService;

        public ExpenseTypeService(IExpenseTypeRepository expenseTypeRepository, IMapper mapper, ICompanyService companyService)
        {
            _expenseTypeRepository = expenseTypeRepository;
            _mapper = mapper;
            _companyService = companyService;
        }

        public async Task<bool> Create(CreateExpenseTypeDTO model, string userName)
        {
            var expenseType = _mapper.Map<ExpenseType>(model);
            expenseType.CompanyId = await _companyService.GetCompanyId(userName);
            return await _expenseTypeRepository.Add(expenseType);
        }

        public async Task Delete(int id)
        {
            ExpenseType deletedExpenseType = await _expenseTypeRepository.GetDefault(x => x.Id == id);

            if (deletedExpenseType != null)
            {
                deletedExpenseType.Status = Status.Passive;
                deletedExpenseType.DeleteDate = DateTime.Now;
                await _expenseTypeRepository.Delete(deletedExpenseType);
            }
        }

        public async Task<List<ExpenseTypeVM>> GetExpenseTypes(Guid? id)
        {
            List<ExpenseTypeVM> expenseTypes = (List<ExpenseTypeVM>)await _expenseTypeRepository.GetFilteredList(

               select: x => new ExpenseTypeVM()
               {
                   Id = x.Id,
                   ExpenseTypeName = x.ExpenseTypeName,
               },
             where: x => (x.Status != Status.Passive) && (x.CompanyId == id),
             orderby: x => x.OrderByDescending(x => x.ExpenseTypeName)
               );

            return expenseTypes;
        }

        public async Task Update(UpdateExpenseTypeDTO model, string userName)
        {
            var expenseType = _mapper.Map<ExpenseType>(model);
            expenseType.CompanyId = await _companyService.GetCompanyId(userName);
            expenseType.CreateDate = DateTime.Now;
            await _expenseTypeRepository.Update(expenseType);
        }

        public async Task<UpdateExpenseTypeDTO> GetById(int id)
        {
            ExpenseType expenseType = await _expenseTypeRepository.GetDefault(x => x.Id == id);

            var model = _mapper.Map<UpdateExpenseTypeDTO>(expenseType);

            return model;
        }


    }
}
