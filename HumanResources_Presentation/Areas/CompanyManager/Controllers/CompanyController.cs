using HumanResources_Application.Models.DTOs.AppUserDTOs;
using HumanResources_Application.Models.DTOs.CompanyDTOs;
using HumanResources_Application.Models.VMs.CompanyVMS;
using HumanResources_Application.Service.AppUserServices;
using HumanResources_Application.Service.CompanyServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Dynamic;

namespace HumanResources_Presentation.Areas.CompanyManager.Controllers
{
    [Area("CompanyManager"), Authorize(Roles = "CompanyManager")]
    public class CompanyController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly ICompanyService _companyService;

        public CompanyController(IAppUserService appUserService, ICompanyService companyService)
        {
            _appUserService = appUserService;
            _companyService = companyService;
        }

        // GET: CompanyController
        public async Task<IActionResult> Index()
        {
            var employee = await _appUserService.GetEmployee(User.Identity.Name);
            ViewBag.Employee = employee;                      
            return View(await _companyService.GetById(employee.CompanyId));
        }

       // GET: CompanyController/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
           
            return View(await _companyService.GetById(id));
        }

        // POST: CompanyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CompanyUpdateDTO model)
        {

            if (ModelState.IsValid) 
            {
                try
                {
                    await _companyService.Update(model);
                    TempData["Success"] = "Company updated has been completed successfully.";
                    
                }
                catch(Exception)
                {
                    TempData["Error"] = "Something went wrong";
                    
                }

                return RedirectToAction("Index", "Company", new { Area = "CompanyManager" });

            }
            else
            {
                ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
                TempData["Error"] = "Your Company hasn't been updated";
                return View(model);
            }




           

        }

        
    }
}
