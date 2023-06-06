using AutoMapper;
using HumanResources_Application.Models.DTOs.ExpenseDTOs;
using HumanResources_Application.Models.VMs.ExpenseVMs;
using HumanResources_Application.Service.AppUserServices;
using HumanResources_Domain.Entities;
using HumanResources_Domain.Enums;
using HumanResources_Domain.Repositories;
using HumanResources_Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Service.ExpenseServices
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMapper _mapper;
        private readonly IExpenseTypeRepository _expenseTypeRepository;
        private readonly IAppUserService _appUserService;



        public ExpenseService(IExpenseRepository expenseRepository, IMapper mapper, IAppUserService appUserService, IExpenseTypeRepository expenseTypeRepository)
        {
            _expenseRepository = expenseRepository;
            _mapper = mapper;
            _appUserService = appUserService;
            _expenseTypeRepository = expenseTypeRepository;
        }


        public async Task<bool> Create(CreateExpenseDTO model, string userName)
        {
            Expense expense = _mapper.Map<Expense>(model);
            expense.AppUserId = await _appUserService.GetUserId(userName);
            return await _expenseRepository.Add(expense);
        }
        public async Task Update(UpdateExpenseDTO model)
        {
            var expense = _mapper.Map<Expense>(model);
            expense.CreateDate = DateTime.Now;
            if (expense != null /*&& expense.ApproveStatus == ApproveStatus.InApproval*/)
            {
                if (model.UploadExpenseDocument != null)
                {
                    using var image = Image.Load(model.UploadExpenseDocument.OpenReadStream());

                    Guid guid = Guid.NewGuid();
                    image.Save($"wwwroot/media/expenseDocument/{guid}.jpg");

                    expense.ExpenseDocument = $"/media/expenseDocument/{guid}.jpg";
                }
                else
                {
                    expense.ExpenseDocument = model.ExpenseDocument;
                }

                await _expenseRepository.Update(expense);
            }
        }
        public async Task Delete(int id)
        {
            Expense deletedExpense = await _expenseRepository.GetDefault(x => x.Id == id);
            if (deletedExpense != null && deletedExpense.ApproveStatus == ApproveStatus.InApproval)
            {
                deletedExpense.Status = Status.Passive;
                deletedExpense.DeleteDate = DateTime.Now;
                await _expenseRepository.Delete(deletedExpense);
            }
        }

        //Review  edilecek:

        //Id'ye göre expense getirme 
        public async Task<UpdateExpenseDTO> GetById(int id)
        {
            Expense expense = await _expenseRepository.GetDefault(x => x.Id == id);

            var model = _mapper.Map<UpdateExpenseDTO>(expense);

            return model;
        }
        public async Task<List<ExpenseVM>> GetExpenses(Guid? id)
        {

            List<ExpenseVM> expenses = (List<ExpenseVM>)await _expenseRepository.GetFilteredList(

               select: x => new ExpenseVM()
               {
                   Id = x.Id,
                   FullName = x.AppUser.FullName,
                   ExpenseAmount = x.ExpenseAmount,
                   ApproveStatus = x.ApproveStatus,
                   CreateDate = x.CreateDate.ToShortDateString(),
                   Status = x.Status,
                   AppUserId = x.AppUser.Id,
                   Explanation = x.Explanation,
                   ExecutiveId = x.AppUser.ExecutiveId,
                   ExecutiveName = x.AppUser.Executive.FullName,
                   ExpenseDate = x.ExpenseDate.ToShortDateString(),
                   ExpenseDocument = x.ExpenseDocument,
                   ExpenseTypeId = x.ExpenseTypeId,
                   ExpenseTypeName = x.ExpenseType.ExpenseTypeName

               },
             where: x => (x.Status != Status.Passive) && (x.AppUser.CompanyId == id),
             orderby: x => x.OrderByDescending(x => x.CreateDate),
             include: x => x.Include(x => x.AppUser)
               ); ;
            return expenses;
        }

        public async Task<List<ExpenseVM>> GetExpensesEmployee(Guid? id)
        {

            List<ExpenseVM> expenses = (List<ExpenseVM>)await _expenseRepository.GetFilteredList(

               select: x => new ExpenseVM()
               {
                   Id = x.Id,
                   FullName = x.AppUser.FullName,
                   ExpenseAmount = x.ExpenseAmount,
                   ApproveStatus = x.ApproveStatus,
                   CreateDate = x.CreateDate.ToShortDateString(),
                   Status = x.Status,
                   AppUserId = x.AppUser.Id,
                   Explanation = x.Explanation,
                   ExecutiveId = x.AppUser.ExecutiveId,
                   ExecutiveName = x.AppUser.Executive.FullName,
                   ExpenseDate = x.ExpenseDate.ToShortDateString(),
                   ExpenseDocument = x.ExpenseDocument,
                   ExpenseTypeId = x.ExpenseTypeId,
                   ExpenseTypeName = x.ExpenseType.ExpenseTypeName
               },
             where: x => (x.Status != Status.Passive) && (x.AppUser.ExecutiveId == id),
             orderby: x => x.OrderByDescending(x => x.CreateDate),
             include: x => x.Include(x => x.AppUser)
               );
            return expenses;
        }

        public async Task<List<ExpenseVM>> GetPersonelExpenses(string userName)
        {
            var expenses = await _expenseRepository.GetFilteredList(
            select: x => new ExpenseVM()
            {
                Id = x.Id,
                ExpenseAmount = x.ExpenseAmount,
                ApproveStatus = x.ApproveStatus,
                Explanation = x.Explanation,
                CreateDate = x.CreateDate.ToShortDateString(),
                Status = x.Status,
                AppUserId = x.AppUser.Id,
                ExecutiveId = x.AppUser.ExecutiveId,
                ExecutiveName = x.AppUser.Executive.FullName,
                ExpenseDate = x.ExpenseDate.ToShortDateString(),
                ExpenseDocument = x.ExpenseDocument,
                ExpenseTypeId = x.ExpenseTypeId,
                ExpenseTypeName = x.ExpenseType.ExpenseTypeName
            },
                where: x => x.AppUser.UserName == userName && x.Status != Status.Passive,
                orderby: x => x.OrderByDescending(x => x.CreateDate),
                include: x => x.Include(x => x.AppUser));

            return (List<ExpenseVM>)expenses;

        }

        public async Task Accept(int id)
        {
            Expense expense = await _expenseRepository.GetDefault(x => x.Id == id);
            expense.ApproveStatus = ApproveStatus.Approved;
            await _expenseRepository.Update(expense);
        }

        public async Task Reject(int id)
        {
            Expense expense = await _expenseRepository.GetDefault(x => x.Id == id);
            expense.ApproveStatus = ApproveStatus.Denied;
            await _expenseRepository.Update(expense);
        }

        public async Task<List<ExpenseVM>> GetExpensesCompanyManagerAwatingApproval(Guid? id)
        {

            List<ExpenseVM> expenses = (List<ExpenseVM>)await _expenseRepository.GetFilteredList(

               select: x => new ExpenseVM()
               {
                   Id = x.Id,
                   FullName = x.AppUser.FullName,
                   ExpenseAmount = x.ExpenseAmount,
                   ApproveStatus = x.ApproveStatus,
                   CreateDate = x.CreateDate.ToShortDateString(),
                   Status = x.Status,
                   AppUserId = x.AppUser.Id,
                   Explanation = x.Explanation,
                   ExecutiveId = x.AppUser.ExecutiveId,
                   ExecutiveName = x.AppUser.Executive.FullName,
                   ExpenseDate = x.ExpenseDate.ToShortDateString(),
                   ExpenseDocument = x.ExpenseDocument,

               },
             where: x => (x.ApproveStatus == ApproveStatus.InApproval) && (x.Status != Status.Passive) && (x.AppUser.CompanyId == id),
             orderby: x => x.OrderByDescending(x => x.CreateDate),
             include: x => x.Include(x => x.AppUser)
               ); ;
            return expenses;
        }

        public async Task<List<ExpenseVM>> GetExpensesManagerAwatingApproval(Guid? id)
        {
            List<ExpenseVM> expenses = (List<ExpenseVM>)await _expenseRepository.GetFilteredList(

               select: x => new ExpenseVM()
               {
                   Id = x.Id,
                   FullName = x.AppUser.FullName,
                   ExpenseAmount = x.ExpenseAmount,
                   ApproveStatus = x.ApproveStatus,
                   CreateDate = x.CreateDate.ToShortDateString(),
                   Status = x.Status,
                   AppUserId = x.AppUser.Id,
                   Explanation = x.Explanation,
                   ExecutiveId = x.AppUser.ExecutiveId,
                   ExecutiveName = x.AppUser.Executive.FullName,
                   ExpenseDate = x.ExpenseDate.ToShortDateString(),
                   ExpenseDocument = x.ExpenseDocument,

               },
             where: x => (x.ApproveStatus == ApproveStatus.InApproval) && (x.Status != Status.Passive) && (x.AppUser.ExecutiveId == id),
             orderby: x => x.OrderByDescending(x => x.CreateDate),
             include: x => x.Include(x => x.AppUser)
               ); ;
            return expenses;
        }

    }
}
