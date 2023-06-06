using HumanResources_Application.Models.DTOs.LeaveTypeDTOs;
using HumanResources_Application.Service.AppUserServices;
using HumanResources_Application.Service.CompanyServices;
using HumanResources_Application.Service.LeaveTypeServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using X.PagedList;

namespace HumanResources_Presentation.Areas.CompanyManager.Controllers
{
    [Authorize]
    [Area("CompanyManager")]
    [Authorize(Roles = "CompanyManager, SiteManager")]
    public class LeaveTypeController : Controller
    {
        private readonly ILeaveTypeService _leaveTypeService;
        private readonly IAppUserService _appUserService;
        private readonly ICompanyService _companyServices;

        public LeaveTypeController(ILeaveTypeService leaveTypeService, IAppUserService appUserService, ICompanyService companyServices)
        {
            _leaveTypeService = leaveTypeService;
            _appUserService = appUserService;
            _companyServices = companyServices;
        }

        public async Task<IActionResult> ListLeaveType(int page = 1)
        {
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            Guid? companyId = await _companyServices.GetCompanyId(User.Identity.Name);
            var leaveTypes = await _leaveTypeService.GetLeaveTypes(companyId);
            return View((leaveTypes).ToPagedList(page, 10));
        }

        [HttpGet]
        public async Task<IActionResult> CreateLeaveType()
        {
            Guid? companyId = await _companyServices.GetCompanyId(User.Identity.Name);
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            ViewBag.LeaveTypes = new SelectList(await _leaveTypeService.GetLeaveTypes(companyId), "Id", "LeaveTypeName");
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLeaveType(CreateLeaveTypeDTO model)
        {
            Guid? companyId = await _companyServices.GetCompanyId(User.Identity.Name);
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            ViewBag.LeaveTypes = new SelectList(await _leaveTypeService.GetLeaveTypes(companyId), "Id", "LeaveTypeName");

            if (ModelState.IsValid)
            {
                var result = await _leaveTypeService.Create(model, User.Identity.Name);

                if (result)
                {
                    TempData["success"] = "Leave type was created successfully.";
                    return RedirectToAction("ListLeaveType", "LeaveType", new { Area = "CompanyManager" });
                }
                else
                {
                    TempData["error"] = "Something goes wrong, leave type could not be created.";
                }
            }
            List<string> errors = new List<string>();
            foreach (var item in ModelState.Values.SelectMany(x => x.Errors))
            {
                errors.Add(item.ErrorMessage);
            }
            TempData["modelError"] = errors.ToArray();
            return RedirectToAction("CreateLeaveType", "LeaveType", new { Area = "CompanyManager" });

        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteComfirmed(IFormCollection collection)
        {
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            int id = int.Parse(collection["Id"]);
            await _leaveTypeService.Delete(id);
            TempData["success"] = "ELeave type was deleted succesfully.";
            return RedirectToAction("ListLeaveType", "LeaveType");
        }



        [HttpGet]
        public async Task<IActionResult> LeaveTypeEdit(int id)
        {
            Guid? companyId = await _companyServices.GetCompanyId(User.Identity.Name);
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            ViewBag.LeaveTypes = new SelectList(await _leaveTypeService.GetLeaveTypes(companyId), "Id", "LeaveTypeName");
            var leaveType = await _leaveTypeService.GetById(id);
            return View(leaveType);
        }

        [HttpPost]
        public async Task<IActionResult> LeaveTypeEdit(UpdateLeaveTypeDTO model)
        {
            Guid? companyId = await _companyServices.GetCompanyId(User.Identity.Name);
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            ViewBag.LeaveTypes = new SelectList(await _leaveTypeService.GetLeaveTypes(companyId), "Id", "LeaveTypeName");

            if (ModelState.IsValid)
            {
                await _leaveTypeService.Update(model, User.Identity.Name);

                TempData["success"] = "Leave type was created successfully.";
                return RedirectToAction("ListLeaveType", "LeaveType", new { Area = "CompanyManager" });
            }
            else
            {
                TempData["error"] = "Something goes wrong, leave type could not be created.";
                return RedirectToAction("LeaveTypeEdit", "LeaveType", new { Area = "CompanyManager" });
            }
        }
    }
}
