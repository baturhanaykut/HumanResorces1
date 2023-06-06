using HumanResources_Application.Models.DTOs.AppUserDTOs;
using HumanResources_Application.Models.VMs.AppUserVMs;
using HumanResources_Application.Models.VMs.EmailVM;
using HumanResources_Application.Service.AppUserServices;
using HumanResources_Application.Service.EmailServices;
using HumanResources_Domain.Entities;
using HumanResources_Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Org.BouncyCastle.Crypto.Macs;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace HumanResources_Presentation.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IEmailService _emailService;
        private readonly UserManager<AppUser> _userManager;

    
        public AccountController(IAppUserService appUserService, IEmailService emailService, UserManager<AppUser> userManager)
        {
            _appUserService = appUserService;
            _emailService = emailService;
            _userManager = userManager;
        }
        //11
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {

            if (User.Identity.IsAuthenticated)
            {
                if (await _appUserService.IsSiteManager(User.Identity.Name))
                {
                    return RedirectToAction("Index", "");
                }
                else if (await _appUserService.IsCompanyManager(User.Identity.Name))
                {
                    return RedirectToAction("Index", "Employee", new { Area = "CompanyManager" });
                }
                else if (await _appUserService.IsEmployee(User.Identity.Name))
                {
                    //TO DO: Employee areası ve sayfası oluşturulduğunda burası düzenlenecektir.
                    return RedirectToAction("Index", "");
                }
            }
            return View();
        }

        [HttpPost, AllowAnonymous, AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _appUserService.Register(registerDTO);
                if (result.Result.Succeeded)
                {
                    var conformationLink = Url.Action("ConfirmEmail", "Account", new { token = result.Token, email = result.Email }, Request.Scheme);
                    //var conformationLink1 = Url.Action("ConfirmEmail", "Account", null, Request.Scheme);

                    var message = new Message(result.Email, "Your membership information", $"Click on the link to log in. {conformationLink} \nYour e - mail address: {result.Email} \nYour password:{result.Password}");
                    var message1 = new Message("staffingadvantage.hr@gmail.com", "New memberhip", "Check for confirmation.");
                    _emailService.SendEmail(message);
                    _emailService.SendEmail(message1);


                    TempData["Conformation"] = "Please check your mailbox and verify your email!";
                    return RedirectToAction("Login", "Account");
                }
                foreach (var item in result.Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                    TempData["Error"] = "Something went wrong.";
                }
            }
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = "/") // Logine nereden geldiğimizi tutar
        {

            if (User.Identity.IsAuthenticated) // Eğer hali hazırda sistemde Authenticate olmuş bir kullanıcı varsa Login sayfasını görmesin.
            {
                if (await _appUserService.IsSiteManager(User.Identity.Name))
                {

                    return RedirectToAction("Index", "Company", new { Area = "SiteManager" });
                }

                else if (await _appUserService.IsCompanyManager(User.Identity.Name))
                {
                    return RedirectToAction("IndexGrid", "CompanyManager", new { Area = "CompanyManager" });
                }

                else if (await _appUserService.IsEmployee(User.Identity.Name))
                {
                    //TO DO: Employee areası ve sayfası oluşturulduğunda burası düzenlenecektir.
                    return RedirectToAction("Index", "Employee", new { Area = "Employee" });
                }
            }
            ViewData["ReturnUrl"] = returnUrl;


            return View();
        }
        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await _appUserService.GetEmployee(model.Email);
                if (user != null)
                {
                    if (user.Status == Status.Active)
                    {

                        SignInResult result = await _appUserService.Login(model);

                        if (result.Succeeded)
                        {
                            if (await _appUserService.IsSiteManager(model.Email))
                            {

                                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                                {
                                    return RedirectToLocal(returnUrl);
                                }
                                return RedirectToAction("Index", "SiteManager", new { Area = "SiteManager" });
                            }

                            else if (await _appUserService.IsCompanyManager(model.Email))
                            {
                                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                                {
                                    return RedirectToLocal(returnUrl);
                                }
                                return RedirectToAction("IndexGrid", "CompanyManager", new { Area = "CompanyManager" });
                            }
                            //TO DO: Employee areası ve sayfası oluşturulduğunda burası düzenlenecektir.
                            else if (await _appUserService.IsEmployee(model.Email))
                            {
                                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                                {
                                    return RedirectToLocal(returnUrl);
                                }
                                return RedirectToAction("Index", "Employee", new { Area = "Employee" });
                            }
                        }

                        ModelState.AddModelError("", "Invalid Login Attempt");
                    }
                }
            }
            ViewData["ReturnUrl"] = returnUrl;
            TempData["Error"] = "Your email address or password is incorrect.\n Or you may not have access yet";
            return View(model);

        }
        private IActionResult RedirectToLocal(string returnUrl = "/")
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Login", "Account");
                //return RedirectToAction("Index", "");
            }
        }
        public async Task<IActionResult> LogOut()
        {
            await _appUserService.LogOut();
            return RedirectToAction("Login", "Account"); // Aynı dizindeki Home Controller'daki Index Actiona'a git
        }
        public async Task<IActionResult> Edit(string userName)
        {

            if (userName != null)
            {
                UpdateProfileDTO user = await _appUserService.GetByUserName(userName);
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
                return RedirectToAction("Index", "");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProfileDTO model)
        {


            if (ModelState.IsValid)
            {
                try
                {
                    TempData["Success"] = "Membership updated has been completed successfully.";
                    await _appUserService.UpdateUser(model);

                }
                catch (Exception)
                {
                    TempData["Error"] = "Something went rong";

                }
                //return RedirectToAction("Index","Home");
                return View("Edit", model);
            }
            else
            {
                TempData["Error"] = "Your profile hasn't been updated";
                return View();
            }

        }
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var result = await _appUserService.ConfirmEmail(token, email); // ToDo: resulta göre sayfa yapılacak

            if (result.Succeeded)
            {
                TempData["Success"] = "Your account was confirmed successfully.";
                return RedirectToAction("Login");

            }
            TempData["Error"] = "Your account could not confirmed. Please register again.";
            return RedirectToAction("register");

        }
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await _appUserService.ForgotPassword(model.Email);

                if (result.Result.Succeeded == false)
                {
                    TempData["Error"] = "Your e-mail is not found. Please check your e-mail.";
                    return View(model);
                }

                var conformationLink = Url.Action("ResetPassword", "Account", new { Code = result.Token, email = result.Email }, Request.Scheme);
                var message = new Message(result.Email, "To reset your password, please click the link below:", $" {conformationLink}");
                _emailService.SendEmail(message);

                TempData["Conformation"] = "Please check your mailbox and verify your email!";
                return RedirectToAction("ResetPasswordConfirmation", "Account");

            }
            return View(model);
        }


        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(string email, string Code)
        {
            var model = new ResetPasswordVM();

            if (email != null && Code != null)
            {
                model.Code = Code;
                model.Email = email;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);

                if (result.Succeeded)
                {
                    TempData["Conformation"] = "Password is updated!";
                    return RedirectToAction("Login", "Account");
                }
            }

            TempData["Error"] = "Your account could not confirmed. Please register again.";
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
























    }
}




