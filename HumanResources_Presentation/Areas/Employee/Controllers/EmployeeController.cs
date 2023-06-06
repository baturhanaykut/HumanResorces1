using HumanResources_Application.Models.DTOs.AppUserDTOs;
using HumanResources_Application.Models.VMs;
using HumanResources_Application.Models.VMs.AdvanceVMs;
using HumanResources_Application.Models.VMs.AppUserVMs;
using HumanResources_Application.Models.VMs.ExpenseVMs;
using HumanResources_Application.Models.VMs.LeaveVMs;
using HumanResources_Application.Service.AddresServices;
using HumanResources_Application.Service.AdvanceService;
using HumanResources_Application.Service.AppUserServices;
using HumanResources_Application.Service.CompanyServices;
using HumanResources_Application.Service.DepartmentService;
using HumanResources_Application.Service.ExpenseServices;
using HumanResources_Application.Service.LeaveServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Runtime.CompilerServices;
using X.PagedList;

namespace HumanResources_Presentation.Areas.Employee.Controllers
{
    [Area("Employee"), Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IAddressService _addressService;
        private readonly IDepartmentService _departmentService;
        private readonly ICompanyService _companyServices;
        private readonly IAdvanceService _advanceService;
        private readonly ILeaveService _leaveService;
        private readonly IExpenseService _expenseService;


        public EmployeeController(IAppUserService appUserService, IAddressService addressService, IDepartmentService departmentService, ICompanyService companyServices, IAdvanceService advanceService, ILeaveService leaveService, IExpenseService expenseService)
        {
            _appUserService = appUserService;
            _addressService = addressService;
            _departmentService = departmentService;
            _companyServices = companyServices;
            _advanceService = advanceService;
            _leaveService = leaveService;
            _expenseService = expenseService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var employee = await _appUserService.GetEmployee(User.Identity.Name);
            ViewBag.Employee = employee;
            Guid? companyId = await _companyServices.GetCompanyId(User.Identity.Name);

            List<EmployeeVM> employee1 = await _appUserService.GetEmployees(companyId);
            List<AdvanceVM> advance = await _advanceService.GetAdvancesManagerAwatingApproval(employee.Id);
            List<ExpenseVM> expense = await _expenseService.GetExpensesManagerAwatingApproval(employee.Id);
            List<LeaveVM> leave = await _leaveService.GetLeavesManagerAwatingApproval(employee.Id);

            ViewBag.EmployeeCount = employee1.Count();
            ViewBag.AdvanceCount = advance.Count();
            ViewBag.ExpenseCount = expense.Count();
            ViewBag.LeaveCount = leave.Count();

            ViewModel vm = new ViewModel();
            vm.Employee = (await _appUserService.GetEmployees(companyId)).ToPagedList(page, 3);
            vm.Advance = (await _advanceService.GetAdvances(employee.Id));
            vm.Leave = (await _leaveService.GetLeaveEmployee(employee.Id));
            vm.Expense = (await _expenseService.GetExpensesEmployee(employee.Id));

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string userName)
        {
            ViewBag.Cities = new SelectList(await _addressService.GetCities(), "Id", "Name");
            ViewBag.Districts = new SelectList(await _addressService.GetDistricts(), "Id", "Name");
            ViewBag.Departments = new SelectList(await _departmentService.GetDepartments(), "Id", "Name");

            var employee = await _appUserService.GetEmployee(User.Identity.Name);
            ViewBag.Employee = employee;
            ViewBag.Executive = new SelectList(await _appUserService.GetExecutive(employee.CompanyId), "Id", "FullName");

            ViewBag.BaseUrl = Request.Scheme + "://" + HttpContext.Request.Host.ToString();

            if (userName != null)
            {

                UpdateProfileDTO user = await _appUserService.GetByUserName(userName);

                //ViewBag["UserCityName"] = await _addressService.


                return View(user);
            }
            else if (userName == "")
            {
                userName = HttpContext.User.Identity.Name;
                UpdateProfileDTO user = await _appUserService.GetByUserName(userName);

                return View(user);
            }
            else
            {
                //return RedirectToAction("Index", nameof(Areas.Member.Controllers.HomeController));
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProfileDTO model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    await _appUserService.UpdateUser(model);
                    TempData["Success"] = "Membership updated has been completed successfully.";

                }
                catch (Exception)
                {
                    TempData["Error"] = "Something went wrong";

                }


                return RedirectToAction("Index", "Employee", new { Area = "Employee" });

            }
            else
            {
                ViewBag.Cities = new SelectList(await _addressService.GetCities(), "Id", "Name");
                ViewBag.Districts = new SelectList(await _addressService.GetDistricts(), "Id", "Name");
                ViewBag.Departments = new SelectList(await _departmentService.GetDepartments(), "Id", "Name");
                ViewBag.BaseUrl = Request.Scheme + "://" + HttpContext.Request.Host.ToString();

                var employee = await _appUserService.GetEmployee(User.Identity.Name);
                ViewBag.Employee = employee;
                ViewBag.Executive = new SelectList(await _appUserService.GetExecutive(employee.CompanyId), "Id", "FullName");

                TempData["Error"] = "Your profile hasn't been updated";
                return View(model);
            }

        }


        [HttpGet]
        public async Task<JsonResult> setDropDownList(int id)
        {
            var districts = await _addressService.GetDistricts(id);
            return Json(districts);
        }

        public async Task<IActionResult> ListEmployees()
        {
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            Guid? companyId = await _companyServices.GetCompanyId(User.Identity.Name);
            return View(await _appUserService.GetEmployees(companyId));
        }

    }
}
