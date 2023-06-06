using HumanResources_Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HumanResources_Presentation.Controllers
{

    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
    }
}