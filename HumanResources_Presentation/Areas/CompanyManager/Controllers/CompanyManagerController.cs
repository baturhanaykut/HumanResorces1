using Autofac.Core;
using HumanResources_Application.Models.DTOs.AppUserDTOs;
using HumanResources_Application.Models.VMs;
using HumanResources_Application.Models.VMs.AdvanceVMs;
using HumanResources_Application.Models.VMs.AppUserVMs;
using HumanResources_Application.Models.VMs.EmailVM;
using HumanResources_Application.Models.VMs.ExpenseVMs;
using HumanResources_Application.Models.VMs.LeaveVMs;
using HumanResources_Application.Service.AddresServices;
using HumanResources_Application.Service.AdvanceService;
using HumanResources_Application.Service.AppUserServices;
using HumanResources_Application.Service.CompanyServices;
using HumanResources_Application.Service.DepartmentService;
using HumanResources_Application.Service.EmailServices;
using HumanResources_Application.Service.ExpenseServices;
using HumanResources_Application.Service.LeaveServices;
using HumanResources_Domain.Repositories;
using HumanResources_Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace HumanResources_Presentation.Areas.Manager.Controllers
{
    [Area("CompanyManager"), Authorize(Roles = "CompanyManager")]
    public class CompanyManagerController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IAddressService _addressService;
        private readonly IDepartmentService _departmentService;
        private readonly ICompanyService _companyServices;
        private readonly IEmailService _emailService;
        private readonly IAdvanceService _advanceService;
        private readonly IExpenseService _expenseService;
        private readonly ILeaveService _leaveService;

        public CompanyManagerController(IAppUserService appUserService, IAddressService addressService, IDepartmentService departmentService, IEmailService emailService, ICompanyService companyServices, IAdvanceService advanceService, IExpenseService expenseService, ILeaveService leaveService)
        {
            _appUserService = appUserService;
            _addressService = addressService;
            _departmentService = departmentService;
            _emailService = emailService;
            _companyServices = companyServices;
            _advanceService = advanceService;
            _expenseService = expenseService;
            _leaveService = leaveService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            Guid? companyId = await _companyServices.GetCompanyId(User.Identity.Name);
            return View(await _appUserService.GetEmployees(companyId));
        }

        //public async Task<IActionResult> IndexGrid()
        //{
        //    ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
        //    Guid companyId = await _companyServices.GetCompanyId(User.Identity.Name);
        //    return View(await _appUserService.GetEmployees(companyId));
        //}

        public async Task<IActionResult> IndexGrid(int page = 1)
        {
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            Guid? companyId = await _companyServices.GetCompanyId(User.Identity.Name);

            List<EmployeeVM> employee = await _appUserService.GetEmployees(companyId);
            List<AdvanceVM> advance = await _advanceService.GetAdvancesCompanyManagerAwatingApproval(companyId);
            List<ExpenseVM> expense = await _expenseService.GetExpensesCompanyManagerAwatingApproval(companyId);
            List<LeaveVM> leave = await _leaveService.GetLeavesCompanyManagerAwatingApproval(companyId);

            ViewBag.EmployeeCount = employee.Count();
            ViewBag.AdvanceCount = advance.Count();
            ViewBag.ExpenseCount = expense.Count();
            ViewBag.LeaveCount = leave.Count();
            ViewModel vm = new ViewModel();
            vm.Employee = (await _appUserService.GetEmployees(companyId)).ToPagedList(page, 3);
            vm.Advance = (await _advanceService.GetAdvancesCompanyManager(companyId));
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string userName)
        {
            ViewBag.Cities = new SelectList(await _addressService.GetCities(), "Id", "Name");
            ViewBag.Districts = new SelectList(await _addressService.GetDistricts(), "Id", "Name");
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
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


                return RedirectToAction("Edit", "CompanyManager", new { Area = "CompanyManager" });

            }
            else
            {
                ViewBag.Cities = new SelectList(await _addressService.GetCities(), "Id", "Name");
                ViewBag.Districts = new SelectList(await _addressService.GetDistricts(), "Id", "Name");
                ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
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

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = new SelectList(await _departmentService.GetDepartments(), "Id", "Name");
            ViewBag.Cities = new SelectList(await _addressService.GetCities(), "Id", "Name");
            ViewBag.Districts = new SelectList(await _addressService.GetDistricts(), "Id", "Name");

            var employee = await _appUserService.GetEmployee(User.Identity.Name);
            ViewBag.Employee = employee;
            ViewBag.Executive = new SelectList(await _appUserService.GetExecutive(employee.CompanyId), "Id", "FullName");
            ViewBag.BaseUrl = Request.Scheme + "://" + HttpContext.Request.Host.ToString();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _appUserService.CreateUser(model);

                if (result.Result.Succeeded)
                {
                    var conformationLink = Url.Action("ConfirmEmail", "Account", new { token = result.Token, email = result.Email, Area = "" }, Request.Scheme);

                    var message = new Message(result.Email, "Your membership information", $"Click on the link to log in. {conformationLink} \nYour e - mail address: {result.Email} \nYour password:{result.Password}");
                    _emailService.SendEmail(message);

                    TempData["Conformation"] = "Please check your mailbox and verify your email!";
                }


                TempData["Success"] = "Membership created has been completed successfully.";
                return RedirectToAction("Index", "CompanyManager");
            }
            TempData["Error"] = "Something went wrong";
            ViewBag.Departments = new SelectList(await _departmentService.GetDepartments(), "Id", "Name");
            ViewBag.Cities = new SelectList(await _addressService.GetCities(), "Id", "Name");
            ViewBag.Districts = new SelectList(await _addressService.GetDistricts(), "Id", "Name");

            var employee = await _appUserService.GetEmployee(User.Identity.Name);
            ViewBag.Employee = employee;
            ViewBag.Executive = new SelectList(await _appUserService.GetExecutive(employee.CompanyId), "Id", "FullName");

            ViewBag.BaseUrl = Request.Scheme + "://" + HttpContext.Request.Host.ToString();
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> EmployeeEdit(string userName)
        {
            ViewBag.Cities = new SelectList(await _addressService.GetCities(), "Id", "Name");
            ViewBag.Districts = new SelectList(await _addressService.GetDistricts(), "Id", "Name");

            var employee = await _appUserService.GetEmployee(User.Identity.Name);
            ViewBag.Employee = employee;
            ViewBag.Executive = new SelectList(await _appUserService.GetExecutive(employee.CompanyId), "Id", "FullName");

            ViewBag.Departments = new SelectList(await _departmentService.GetDepartments(), "Id", "Name");
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
        public async Task<IActionResult> EmployeeEdit(UpdateProfileDTO model)
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


                return RedirectToAction("Edit", "CompanyManager", new { Area = "CompanyManager" });

            }
            else
            {
                ViewBag.Cities = new SelectList(await _addressService.GetCities(), "Id", "Name");
                ViewBag.Districts = new SelectList(await _addressService.GetDistricts(), "Id", "Name");

                var employee = await _appUserService.GetEmployee(User.Identity.Name);
                ViewBag.Employee = employee;
                ViewBag.Executive = new SelectList(await _appUserService.GetExecutive(employee.CompanyId), "Id", "FullName");
                ViewBag.Departments = new SelectList(await _departmentService.GetDepartments(), "Id", "Name");
                ViewBag.BaseUrl = Request.Scheme + "://" + HttpContext.Request.Host.ToString();
                TempData["Error"] = "Your profile hasn't been updated";
                return View(model);
            }

        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteComfirmed(IFormCollection collection)
        {
            Guid id = Guid.Parse(collection["Id"]);
            await _appUserService.Delete(id);
            TempData["success"] = "Employee was deleted succesfully.";
            return RedirectToAction("Index", "CompanyManager");


        }

        public async Task<IActionResult> Reports()
        {
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            Guid? companyId = await _companyServices.GetCompanyId(User.Identity.Name);
            return View(await _appUserService.GetEmployees(companyId));
        }


    }
}