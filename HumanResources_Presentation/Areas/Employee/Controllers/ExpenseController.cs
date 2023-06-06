using AutoMapper;
using HumanResources_Application.Models.DTOs.ExpenseDTOs;
using HumanResources_Application.Models.VMs.EmailVM;
using HumanResources_Application.Service.AppUserServices;
using HumanResources_Application.Service.CompanyServices;
using HumanResources_Application.Service.EmailServices;
using HumanResources_Application.Service.ExpenseServices;
using HumanResources_Application.Service.ExpenseTypeServices;
using HumanResources_Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace HumanResources_Presentation.Areas.Employee.Controllers
{
    [Authorize]
    [Area("Employee")]
    [Authorize(Roles = "Employee, CompanyManager")]
    public class ExpenseController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IExpenseService _expenseService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IExpenseTypeService _expenseTypeService;
        private readonly ICompanyService _companyServices;

        public ExpenseController(IAppUserService appUserService, IExpenseService expenseService, IExpenseRepository expenseRepository, IMapper mapper, IEmailService emailService, IExpenseTypeService expenseTypeService, ICompanyService companyServices)
        {
            _appUserService = appUserService;
            _expenseService = expenseService;
            _mapper = mapper;
            _emailService = emailService;
            _expenseTypeService = expenseTypeService;
            _companyServices = companyServices;
        }

        public async Task<IActionResult> ListExpense()
        {
            var employeeExpenses = await _expenseService.GetPersonelExpenses(User.Identity.Name);
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            return View(employeeExpenses);
        }

        [HttpGet]
        public async Task<IActionResult> CreateExpense()
        {
            Guid? companyId = await _companyServices.GetCompanyId(User.Identity.Name);
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            ViewBag.ExpenseTypes = new SelectList(await _expenseTypeService.GetExpenseTypes(companyId), "Id", "ExpenseTypeName");
            return View(); ;
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateExpense(CreateExpenseDTO model)
        {
            Guid? companyId = await _companyServices.GetCompanyId(User.Identity.Name);
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            ViewBag.ExpenseTypes = new SelectList(await _expenseTypeService.GetExpenseTypes(companyId), "Id", "ExpenseTypeName");

            var dayDifference = (decimal)(DateTime.Now - model.ExpenseDate).TotalDays;

            if ((DateTime.Compare(model.ExpenseDate, DateTime.Now) > 0) || (dayDifference > 365))
            {
                TempData["Error"] = "The expense date cannot be more than 1 year past or future date. Check the entered expense date.";
                return View(model);
            }
            else
            {

                if (ModelState.IsValid)
                {
                    var result = await _expenseService.Create(model, User.Identity.Name);
                    var employee = await _appUserService.GetByUserName(User.Identity.Name);
                    var manager = await _appUserService.GetEmployeeId(employee.ExecutiveId);

                    if (result)
                    {
                        if (manager != null)
                        {
                            var conformationLink = Url.Action("Expenses", "EmployeeRequest", new { area = "CompanyManager" }, Request.Scheme);
                            var message = new Message($"{manager.Email}", "Expense Claime", $"Expense request received. Please checked.. \n{conformationLink}");
                            _emailService.SendEmail(message);
                        }

                        TempData["success"] = "Expense was created successfully.";
                        return RedirectToAction("ListExpense", "Expense", new { Area = "Employee" });
                    }
                    else
                    {
                        TempData["error"] = "Something goes wrong, Expense could not be created.";
                    }
                }
                List<string> errors = new List<string>();
                foreach (var item in ModelState.Values.SelectMany(x => x.Errors))
                {
                    errors.Add(item.ErrorMessage);
                }
                TempData["modelError"] = errors.ToArray();
                return RedirectToAction("CreateExpense", "Expense", new { Area = "Employee" });
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteComfirmed(IFormCollection collection)
        {
            int id = int.Parse(collection["Id"]);
            await _expenseService.Delete(id);
            TempData["success"] = "Expense was deleted succesfully.";
            return RedirectToAction("ListExpense", "Expense");
        }

        [HttpGet]
        public async Task<IActionResult> ExpenseEdit(int id)
        {
            Guid? companyId = await _companyServices.GetCompanyId(User.Identity.Name);
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            ViewBag.ExpenseTypes = new SelectList(await _expenseTypeService.GetExpenseTypes(companyId), "Id", "ExpenseTypeName");
            var user = await _expenseService.GetById(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> ExpenseEdit(UpdateExpenseDTO model)
        {
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            var employee = await _appUserService.GetByUserName(User.Identity.Name);
            var manager = await _appUserService.GetEmployeeId(employee.ExecutiveId);

            var dayDifference = (decimal)(DateTime.Now - model.ExpenseDate).TotalDays;

            if ((DateTime.Compare(model.ExpenseDate, DateTime.Now) > 0) || (dayDifference > 365))
            {
                TempData["Error"] = "The expense date cannot be more than 1 year past or future date. Check the entered expense date.";
                return View(model);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        if (manager != null)
                        {
                            var conformationLink = Url.Action("Expenses", "EmployeeRequest", new { area = "CompanyManager" }, Request.Scheme);
                            var message = new Message($"{manager.Email}", "Expense Updated", $"The expense was updated. Please checked.. \n{conformationLink}");
                            _emailService.SendEmail(message);
                        }

                        await _expenseService.Update(model);
                        TempData["Success"] = "Expense updated has been completed successfully.";

                    }
                    catch (Exception)
                    {
                        TempData["Error"] = "Something went wrong.";

                    }
                    return RedirectToAction("ListExpense", "Expense");
                }
                else
                {
                    //ViewBag.ExpenseTypes = new SelectList(await _expenseService.GetExpenseTypes(), "Id", "ExpenseTypeName");
                    TempData["Error"] = "Your ekpense hasn't been updated.";
                    return View(model);
                }
            }
        }

    }
}
