using AutoMapper;
using HumanResources_Application.Models.DTOs.LeaveDTOs;
using HumanResources_Application.Models.VMs.EmailVM;
using HumanResources_Application.Service.AppUserServices;
using HumanResources_Application.Service.EmailServices;
using HumanResources_Application.Service.LeaveServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace HumanResources_Presentation.Areas.Employee.Controllers
{
    [Authorize]
    [Area("Employee")]
    [Authorize(Roles = "Employee, CompanyManager")]
    public class LeaveController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IMapper _mapper;
        private readonly ILeaveService _leaveService;
        private readonly IEmailService _emailService;

        public LeaveController(IAppUserService appUserService, IMapper mapper, ILeaveService leaveServıce, IEmailService emailService)
        {
            _appUserService = appUserService;
            _mapper = mapper;
            _leaveService = leaveServıce;
            _emailService = emailService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            return View();
        }

        public async Task<IActionResult> ListLeave()
        {
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            var employeeLeaves = await _leaveService.GetPersonelLeaves(User.Identity.Name);
            return View(employeeLeaves);
        }

        [HttpGet]
        public async Task<IActionResult> CreateLeave()
        {
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            ViewBag.LeaveTypes = new SelectList(await _leaveService.GetLeaveTypes(), "Id", "LeaveTypeName");

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLeave(CreateLeaveDTO model)
        {
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            ViewBag.LeaveTypes = new SelectList(await _leaveService.GetLeaveTypes(), "Id", "LeaveTypeName");
            decimal daysRequested = (decimal)(model.EndDate - model.StartDate).TotalDays;

            if (DateTime.Compare(model.StartDate, model.EndDate) > 0)
            {
                TempData["Error"] = "The end date of the leave must be after the start date. Please check the entered dates.";
                return View(model);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var result = await _leaveService.Create(model, User.Identity.Name);
                    var employee = await _appUserService.GetByUserName(User.Identity.Name);
                    var manager = await _appUserService.GetEmployeeId(employee.ExecutiveId);

                    if (result)
                    {
                        if (manager != null)
                        {
                            var conformationLink = Url.Action("Leaves", "EmployeeRequest", new { area = "CompanyManager" }, Request.Scheme);
                            var message = new Message($"{manager.Email}", "Leave Claime", $"Leave request received. Please checked.. \n{conformationLink}");
                            _emailService.SendEmail(message);
                        }

                        TempData["Success"] = "Leave was created successfully.";
                        return RedirectToAction("ListLeave", "Leave", new { Area = "Employee" });
                    }
                    else
                    {
                        TempData["Error"] = "Something goes wrong, Leave could not be created.";
                    }
                }

                List<string> errors = new List<string>();
                foreach (var item in ModelState.Values.SelectMany(x => x.Errors))
                {
                    errors.Add(item.ErrorMessage);
                }
                TempData["Error"] = errors.ToArray();

                return RedirectToAction("CreateLeave", "Leave", new { Area = "Employee" });
            }
        }




        [HttpPost, ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(IFormCollection collection)
        {
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            int id = int.Parse(collection["Id"]);
            await _leaveService.Delete(id);
            TempData["success"] = "Leave was deleted succesfully.";
            return RedirectToAction("ListLeave", "Leave");
        }

        [HttpGet]
        public async Task<IActionResult> EditLeave(int id)
        {
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            ViewBag.LeaveTypes = new SelectList(await _leaveService.GetLeaveTypes(), "Id", "LeaveTypeName");

            var user = await _leaveService.GetByID(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditLeave(UpdateLeaveDTO model)
        {
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            var employee = await _appUserService.GetByUserName(User.Identity.Name);
            var manager = await _appUserService.GetEmployeeId(employee.ExecutiveId);

            if (DateTime.Compare(model.StartDate, model.EndDate) > 0)
            {
                TempData["Error"] = "The end date of the leave must be after the start date. Please check the entered dates.";
                return View(model);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    //decimal daysRequested = (decimal)(model.EndDate - model.StartDate).TotalDays;

                    //if (model.LeaveDay > daysRequested)
                    //{
                    //    TempData["Error"] = "The number of days entered is greater than the number of days in the relevant date range. Please check the selected dates.";
                    //    return View(model);
                    //}

                    try
                    {
                        if (manager != null)
                        {
                            var conformationLink = Url.Action("Leaves", "EmployeeRequest", new { area = "CompanyManager" }, Request.Scheme);
                            var message = new Message($"{manager.Email}", "Leave Updated", $"The leave was updated. Please checked.. \n{conformationLink}");
                            _emailService.SendEmail(message);
                        }

                        await _leaveService.Update(model);
                        TempData["Success"] = "Leave updated has been completed successfully.";

                    }
                    catch (Exception)
                    {
                        TempData["Error"] = "Something went wrong.";

                    }
                    return RedirectToAction("ListLeave", "Leave");
                }
                else
                {
                    ViewBag.LeaveTypes = new SelectList(await _leaveService.GetLeaveTypes(), "Id", "LeaveTypeName");
                    ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
                    TempData["Error"] = "Your leave hasn't been updated.";
                    return View(model);
                }
            }

        }

    }
}

