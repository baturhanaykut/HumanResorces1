using HumanResources_Application.Models.DTOs.ExpenseTypeDTOs;
using HumanResources_Application.Service.AppUserServices;
using HumanResources_Application.Service.CompanyServices;
using HumanResources_Application.Service.ExpenseTypeServices;
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
    public class ExpenseTypeController : Controller
    {
        private readonly IExpenseTypeService _expenseTypeService;
        private readonly IAppUserService _appUserService;
        private readonly ICompanyService _companyServices;

        public ExpenseTypeController(IExpenseTypeService expenseTypeService, IAppUserService appUserService, ICompanyService companyServices)
        {
            _expenseTypeService = expenseTypeService;
            _appUserService = appUserService;
            _companyServices = companyServices;
        }

        public async Task<IActionResult> ListExpenseType(int page = 1)
        {
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            Guid? companyId = await _companyServices.GetCompanyId(User.Identity.Name);
            var expenseTypes = await _expenseTypeService.GetExpenseTypes(companyId);
            return View((expenseTypes).ToPagedList(page, 10));
        }


        [HttpGet]
        public async Task<IActionResult> CreateExpenseType()
        {
            Guid? companyId = await _companyServices.GetCompanyId(User.Identity.Name);
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            ViewBag.ExpenseTypes = new SelectList(await _expenseTypeService.GetExpenseTypes(companyId), "Id", "ExpenseTypeName");
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateExpenseType(CreateExpenseTypeDTO model)
        {
            Guid? companyId = await _companyServices.GetCompanyId(User.Identity.Name);
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            ViewBag.ExpenseTypes = new SelectList(await _expenseTypeService.GetExpenseTypes(companyId), "Id", "ExpenseTypeName");

            if (ModelState.IsValid)
            {
                var result = await _expenseTypeService.Create(model, User.Identity.Name);

                if (result)
                {
                    TempData["success"] = "Expense was created successfully.";
                    return RedirectToAction("ListExpenseType", "ExpenseType", new { Area = "CompanyManager" });
                }
                else
                {
                    TempData["error"] = "Something goes wrong, Expense type could not be created.";
                }
            }
            List<string> errors = new List<string>();
            foreach (var item in ModelState.Values.SelectMany(x => x.Errors))
            {
                errors.Add(item.ErrorMessage);
            }
            TempData["modelError"] = errors.ToArray();
            return RedirectToAction("CreateExpenseType", "ExpenseType", new { Area = "CompanyManager" });

        }

       
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteComfirmed(IFormCollection collection)
        {
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            int id = int.Parse(collection["Id"]);
            await _expenseTypeService.Delete(id);
            TempData["success"] = "Expense type was deleted succesfully.";
            return RedirectToAction("ListExpenseType", "ExpenseType");
        }



        [HttpGet]
        public async Task<IActionResult> ExpenseTypeEdit(int id)
        {
            Guid? companyId = await _companyServices.GetCompanyId(User.Identity.Name);
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            ViewBag.ExpenseTypes = new SelectList(await _expenseTypeService.GetExpenseTypes(companyId), "Id", "ExpenseTypeName");
            var expenseType = await _expenseTypeService.GetById(id);
            return View(expenseType);
        }

        [HttpPost]
        public async Task<IActionResult> ExpenseTypeEdit(UpdateExpenseTypeDTO model)
        {
            Guid? companyId = await _companyServices.GetCompanyId(User.Identity.Name);
            ViewBag.Employee = await _appUserService.GetEmployee(User.Identity.Name);
            ViewBag.ExpenseTypes = new SelectList(await _expenseTypeService.GetExpenseTypes(companyId), "Id", "ExpenseTypeName");

            if (ModelState.IsValid)
            {
                await _expenseTypeService.Update(model, User.Identity.Name);

                TempData["success"] = "Expense was created successfully.";
                return RedirectToAction("ListExpenseType", "ExpenseType", new { Area = "CompanyManager" });
            }
            else
            {
                TempData["error"] = "Something goes wrong, Expense type could not be created.";
                return RedirectToAction("ExpenseTypeEdit", "ExpenseType", new { Area = "CompanyManager" });
            }
        }
    }
}
