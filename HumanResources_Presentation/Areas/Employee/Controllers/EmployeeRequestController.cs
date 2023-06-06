using HumanResources_Application.Models.VMs.EmailVM;
using HumanResources_Application.Service.AdvanceService;
using HumanResources_Application.Service.AppUserServices;
using HumanResources_Application.Service.EmailServices;
using HumanResources_Application.Service.ExpenseServices;
using HumanResources_Application.Service.LeaveServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources_Presentation.Areas.Employee.Controllers
{
    [Area("Employee"), Authorize(Roles = "Employee")]
    public class EmployeeRequestController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IAdvanceService _advanceService;
        private readonly IExpenseService _expenseService;
        private readonly ILeaveService _leaveService;
        private readonly IEmailService _emailService;
        public EmployeeRequestController(IAppUserService appUserService, IAdvanceService advanceService, IExpenseService expenseService, ILeaveService leaveService, IEmailService emailService)
        {
            _appUserService = appUserService;
            _advanceService = advanceService;
            _expenseService = expenseService;
            _leaveService = leaveService;
            _emailService = emailService;
        }

        public async Task<IActionResult> Advance()
        {
            var employee = await _appUserService.GetEmployee(User.Identity.Name);

            ViewBag.Employee = employee;

            var employeesAdvances = await _advanceService.GetAdvances(employee.Id);  //giriş yapanın idsi gönderilecek

            return View(employeesAdvances);
        }

        public async Task<IActionResult> Expense()
        {
            var employee = await _appUserService.GetEmployee(User.Identity.Name);

            ViewBag.Employee = employee;

            var employeesExpense = await _expenseService.GetExpensesEmployee(employee.Id);  //giriş yapanın idsi gönderilecek

            return View(employeesExpense);
        }

        public async Task<IActionResult> Leave()
        {
            var employee = await _appUserService.GetEmployee(User.Identity.Name);

            ViewBag.Employee = employee;

            var employeesExpense = await _leaveService.GetLeaveEmployee(employee.Id);  //giriş yapanın idsi gönderilecek

            return View(employeesExpense);
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

                TempData["Conformation"] = "Advance Accepted!";
                return RedirectToAction("Advance", "EmployeeRequest");
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
                var message = new Message($"{user.Email}", "Advance Knowledge", $"The advance has been rejected...\n{conformationLink}");
                _emailService.SendEmail(message);

                TempData["Conformation"] = "Advance Rejected!";
                return RedirectToAction("Advance", "EmployeeRequest");
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

                var message = new Message($"{user.Email}", "Expense Knowledge", $"Your expense has been approved...\n{conformationLink}");
                _emailService.SendEmail(message);

                TempData["Conformation"] = "Expense Accepted!";
                return RedirectToAction("Expense", "EmployeeRequest");
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

                var message = new Message($"{user.Email}", "Expense Knowledge", $"The expense has been rejected...\n{conformationLink}");
                _emailService.SendEmail(message);

                TempData["Conformation"] = "Expense Rejected!";
                return RedirectToAction("Expense", "EmployeeRequest");
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

                TempData["Conformation"] = "Leave Accepted!";
                return RedirectToAction("Leave", "EmployeeRequest");
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

                var message = new Message($"{user.Email}", "Leave Knowledge", $"The leave has been rejected...\n{conformationLink}");
                _emailService.SendEmail(message);

                TempData["Conformation"] = "Leave Rejected!";
                return RedirectToAction("Leave", "EmployeeRequest");
            }
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            TempData["Error"] = "Something went wrong.";
            return View();

        }



    }
}
