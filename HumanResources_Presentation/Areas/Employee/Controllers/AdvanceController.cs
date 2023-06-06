using AutoMapper;
using HumanResources_Application.Models.DTOs.AdvanceDTO;
using HumanResources_Application.Models.VMs.EmailVM;
using HumanResources_Application.Service.AdvanceService;
using HumanResources_Application.Service.AppUserServices;
using HumanResources_Application.Service.EmailServices;
using HumanResources_Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HumanResources_Presentation.Areas.Employee.Controllers
{
    [Authorize]
    [Area("Employee")]
    [Authorize(Roles = "Employee, CompanyManager")]
    public class AdvanceController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IAdvanceService _advanceService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        public AdvanceController(IAppUserService appUserService, IAdvanceService advanceService, IMapper mapper, IEmailService emailServices)
        {
            _appUserService = appUserService;
            _advanceService = advanceService;
            _mapper = mapper;
            _emailService = emailServices;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            return View();
        }

        public async Task<IActionResult> ListAdvance()
        {
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            var employeeAdvances = await _advanceService.GetPersonelAdvances(User.Identity.Name);
            return View(employeeAdvances);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAdvance()
        {
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        
        public async Task<IActionResult> CreateAdvance(CreateAdvanceDTO model)
        {
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);

            if (DateTime.Compare( DateTime.Now , model.PaymentDueDate) > 0)
            {
                TempData["Error"] = "The payent due date cannot be selected before today's date. Please check the entered dates.";
                return View(model);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var result = await _advanceService.Create(model, User.Identity.Name);
                    var employee = await _appUserService.GetByUserName(User.Identity.Name);
                    var manager = await _appUserService.GetEmployeeId(employee.ExecutiveId);

                    if (result)
                    {
                        if (manager != null)
                        {
                            var conformationLink = Url.Action("Index_Advances", "EmployeeRequest", new { area = "CompanyManager" }, Request.Scheme);
                            var message = new Message($"{manager.Email}", "Advance Claime", $"Advanse request received. Please checked.. \n{conformationLink}");
                            _emailService.SendEmail(message);
                        }
                        TempData["Success"] = "Advance was created successfully.";
                        return RedirectToAction("ListAdvance", "Advance", new { Area = "Employee" });
                    }
                    else
                    {
                        TempData["Error"] = "Something goes wrong, Advance could not be created.";
                    }
                }
                List<string> errors = new List<string>();
                foreach (var item in ModelState.Values.SelectMany(x => x.Errors))
                {
                    errors.Add(item.ErrorMessage);
                }
                TempData["Error"] = errors.ToArray();
                return RedirectToAction("CreateAdvance", "Advance", new { Area = "Employee" });


            }

        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(IFormCollection collection)
        {
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            int id = int.Parse(collection["Id"]);
            await _advanceService.Delete(id);
            TempData["Success"] = "Advance was deleted succesfully.";
            return RedirectToAction("ListAdvance", "Advance");
        }

        [HttpGet]
        public async Task<IActionResult> AdvanceEdit(int id)
        {
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            var user = await _advanceService.GetByID(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> AdvanceEdit(UpdateAdvanceDTO model)
        {
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            var employee = await _appUserService.GetByUserName(User.Identity.Name);
            var manager = await _appUserService.GetEmployeeId(employee.ExecutiveId);

            if (ModelState.IsValid)
            {

                try
                {
                    if (manager != null)
                    {
                        var conformationLink = Url.Action("Index_Advances", "EmployeeRequest", new { area = "CompanyManager" }, Request.Scheme);
                        var message = new Message($"{manager.Email}", "Advance Updated", $"The advance was updated. Please checked.. \n{conformationLink}");
                        _emailService.SendEmail(message);
                    }

                    await _advanceService.Update(model);
                    TempData["Success"] = "Advance updated has been completed successfully.";

                }
                catch (Exception)
                {
                    TempData["Error"] = "Something went wrong.";

                }
                return RedirectToAction("ListAdvance", "Advance");
            }
            else
            {
                ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
                TempData["Error"] = "Your advance hasn't been updated.";
                return View(model);
            }
        }

    }
}
