using HumanResources_Application.Models.DTOs.CompanyDTOs;
using HumanResources_Application.Models.VMs.AppUserVMs;
using HumanResources_Application.Models.VMs.CompanyVMS;
using HumanResources_Application.Models.VMs.EmailVM;
using HumanResources_Application.Service.AppUserServices;
using HumanResources_Application.Service.CompanyServices;
using HumanResources_Application.Service.EmailServices;
using HumanResources_Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace HumanResources_Presentation.Areas.SiteManager.Controllers
{
    [Area("SiteManager")]
    [Authorize(Roles = "SiteManager")]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly IAppUserService _appUserService;
        private readonly IEmailService _emailService;

        public CompanyController(ICompanyService companyService, IAppUserService appUserService, IEmailService emailService)
        {

            _companyService = companyService;
            _appUserService = appUserService;
            _emailService = emailService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Data = JsonConvert.SerializeObject(await _companyService.GetAllCompaniesPieChart());
            ViewBag.DataPoints = JsonConvert.SerializeObject(await _companyService.GetAllCompaniesBarChart());
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            return View();
        }

        public async Task<IActionResult> CompanyStatus()
        {
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            var companies = await _companyService.GetAllCompanies();
            return View(companies);
        }

        public async Task<IActionResult> Update(Guid? id)
        {
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            return View(await _companyService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(CompanyUpdateDTO updateCompanyDTO)
        {
            if (ModelState.IsValid)
            {
                await _companyService.Update(updateCompanyDTO);
                TempData["Conformation"] = "Updating Company!";
                return RedirectToAction("Index", "Company");
            }
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            TempData["Error"] = "Something went wrong.";
            return View(updateCompanyDTO);
        }

        public async Task<IActionResult> AktiveCompany(Guid? id)
        {
            var company = await _companyService.GetById(id);

            if (ModelState.IsValid)
            {
                await _companyService.AktiveCompany(id);
                
                var message1 = new Message($"{company.CompanyEmail}", "Company is active", $"You can now login.");
                _emailService.SendEmail(message1);


                TempData["Conformation"] = "Updating Company!";
                return RedirectToAction("CompanyStatus", "Company");
            }
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            TempData["Error"] = "Something went wrong.";
            return View();
        }

        public async Task<IActionResult> DeAktiveCompany(Guid? id)
        {
            var company = await _companyService.GetById(id);

            if (ModelState.IsValid)
            {
                await _companyService.DeAktiveCompany(id);

                var message1 = new Message($"{company.CompanyEmail}", "Company is passive", $"Your company is passive,Your can't login");

                _emailService.SendEmail(message1);


                TempData["Conformation"] = "Updating Company!";
                return RedirectToAction("CompanyStatus", "Company");
            }
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            TempData["Error"] = "Something went wrong.";
            return View();
        }


    }
}

