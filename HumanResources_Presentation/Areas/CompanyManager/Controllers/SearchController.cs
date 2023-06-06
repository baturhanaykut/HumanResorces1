using HumanResources_Application.Service.AppUserServices;
using HumanResources_Application.Service.SearchServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace HumanResources_Presentation.Areas.CompanyManager.Controllers
{
    [Area("CompanyManager"), Authorize(Roles = "CompanyManager, Employee")]
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;
        private readonly IAppUserService _appUserService;

        public SearchController(ISearchService searchService, IAppUserService appUserService)
        {
            _searchService = searchService;
            _appUserService = appUserService;
        }

        public async Task<IActionResult> Index(string employeeName, Guid companyId)
        {
            if (!employeeName.IsNullOrEmpty())
            {
                var employee = await _appUserService.GetEmployee(User.Identity.Name);
                ViewBag.Employee = employee;
                return View(await _searchService.GetSearchResult(employeeName, employee.CompanyId));
            }
            return RedirectToAction("IndexGrid","Employee");
            
        }
    }
}
