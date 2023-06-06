using HumanResources_Application.Models.VMs.EmailVM;
using HumanResources_Application.Service.AdvanceService;
using HumanResources_Application.Service.AppUserServices;
using HumanResources_Application.Service.CompanyServices;
using HumanResources_Application.Service.EmailServices;
using HumanResources_Application.Service.ExpenseServices;
using HumanResources_Application.Service.LeaveServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources_Presentation.Areas.CompanyManager.Controllers
{
    [Area("CompanyManager"), Authorize(Roles = "CompanyManager")]

    public class EmployeeRequestController : Controller
    {

        private readonly IAppUserService _appUserService;
        private readonly ICompanyService _companyService;
        private readonly IAdvanceService _advanceService;
        private readonly IExpenseService _expenseService;
        private readonly ILeaveService _leaveService;
        private readonly IEmailService _emailService;

        public EmployeeRequestController(IAppUserService appUserService, ICompanyService companyService, IAdvanceService advanceService, IExpenseService expenseService, ILeaveService leaveService, IEmailService emailService)
        {
            _appUserService = appUserService;
            _companyService = companyService;
            _advanceService = advanceService;
            _expenseService = expenseService;
            _leaveService = leaveService;
            _emailService = emailService;
        }
        public async Task<IActionResult> Index_Advances()
        {
            var employee = await _appUserService.GetEmployee(User.Identity.Name);

            ViewBag.Employee = employee;

            var employeesAdvances = await _advanceService.GetAdvancesCompanyManager(employee.CompanyId);  //giriş yapanın idsi gönderilecek

            return View(employeesAdvances);
        }
        public async Task<IActionResult> Expenses()
        {
            var employee = await _appUserService.GetEmployee(User.Identity.Name);

            ViewBag.Employee = employee;

            var employeesExpenses = await _expenseService.GetExpenses(employee.CompanyId);

            return View(employeesExpenses);
        }
        public async Task<IActionResult> Leaves()
        {
            var employee = await _appUserService.GetEmployee(User.Identity.Name);

            ViewBag.Employee = employee;

            var employeesLeaves = await _leaveService.GetLeaves(employee.CompanyId);

            return View(employeesLeaves);
        }
        public async Task<IActionResult> AcceptAdvance(int id)
        {
            var advance = await _advanceService.GetByID(id);
            var user = await _appUserService.GetEmployeeId(advance.AppUserId);

            if (ModelState.IsValid)
            {
                await _advanceService.Accept(id);

                var conformationLink = Url.Action("ListAdvance", "Advance", new { area = "Employee" }, Request.Scheme);
                
                var message = new Message($"{user.Email}", "Advance Knowledge", $"Your advance has been approved...\n{conformationLink}");
                _emailService.SendEmail(message);

                TempData["Conformation"] = "Updating Advance!";
                return RedirectToAction("Index_Advances", "EmployeeRequest");
            }
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            TempData["Error"] = "Something went wrong.";
            return View();
        }
        public async Task<IActionResult> RejectAdvance(int id)
        {
            var advance = await _advanceService.GetByID(id);
            var user = await _appUserService.GetEmployeeId(advance.AppUserId);

            if (ModelState.IsValid)
            {
                await _advanceService.Reject(id);
                var conformationLink = Url.Action("ListAdvance", "Advance", new { area = "Employee" }, Request.Scheme);
                var message = new Message($"{user.Email}", "Advance Knowledge", $"Your advance has been reject...\n{conformationLink}");
                _emailService.SendEmail(message);

                TempData["Conformation"] = "Updating Advance!";
                return RedirectToAction("Index_Advances", "EmployeeRequest");
            }
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            TempData["Error"] = "Something went wrong.";
            return View();
        }
        public async Task<IActionResult> AcceptExpense(int id)
        {
            var expense = await _expenseService.GetById(id);
            var user = await _appUserService.GetEmployeeId(expense.AppUserId);

            if (ModelState.IsValid)
            {
                await _expenseService.Accept(id);

                var conformationLink = Url.Action("ListExpense", "Expense", new { area = "Employee" }, Request.Scheme);

                var message = new Message($"{user.Email}", "Expense Knowledge", $"Your advance has been approved...\n{conformationLink}");
                _emailService.SendEmail(message);

                TempData["Conformation"] = "Updating Expense!";
                return RedirectToAction("Expenses", "EmployeeRequest");
            }
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            TempData["Error"] = "Something went wrong.";
            return View();

        }
        public async Task<IActionResult> RejectExpense(int id)
        {
            var expense = await _expenseService.GetById(id);
            var user = await _appUserService.GetEmployeeId(expense.AppUserId);

            if (ModelState.IsValid)
            {
                await _expenseService.Reject(id);

                var conformationLink = Url.Action("ListExpense", "Expense", new { area = "Employee" }, Request.Scheme);

                var message = new Message($"{user.Email}", "Expense Knowledge", $"Your advance has has been reject...\n{conformationLink}");
                _emailService.SendEmail(message);

                TempData["Conformation"] = "Updating Expense!";
                return RedirectToAction("Expenses", "EmployeeRequest");
            }
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            TempData["Error"] = "Something went wrong.";
            return View();

        }
        public async Task<IActionResult> AcceptLeave(int id)
        {
            var leave = await _leaveService.GetByID(id);
            var user = await _appUserService.GetEmployeeId(leave.AppUserId);

            if (ModelState.IsValid)
            {
                await _leaveService.Accept(id);

                var conformationLink = Url.Action("ListLeave", "Leave", new { area = "Employee" }, Request.Scheme);

                var message = new Message($"{user.Email}", "Leave Knowledge", $"Your leave has been approved...\n{conformationLink}");
                _emailService.SendEmail(message);

                TempData["Conformation"] = "Updating Leave!";
                return RedirectToAction("Leaves", "EmployeeRequest");
            }
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            TempData["Error"] = "Something went wrong.";
            return View();

        }
        public async Task<IActionResult> RejectLeave(int id)
        {
            var leave = await _leaveService.GetByID(id);
            var user = await _appUserService.GetEmployeeId(leave.AppUserId);

            if (ModelState.IsValid)
            {
                await _leaveService.Reject(id);

                var conformationLink = Url.Action("ListLeave", "Leave", new { area = "Employee" }, Request.Scheme);

                var message = new Message($"{user.Email}", "Leave Knowledge", $"Your leave has been reject...\n{conformationLink}");
                _emailService.SendEmail(message);

                TempData["Conformation"] = "Updating Leave!";
                return RedirectToAction("Leaves", "EmployeeRequest");
            }
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            TempData["Error"] = "Something went wrong.";
            return View();

        }


    }
}
