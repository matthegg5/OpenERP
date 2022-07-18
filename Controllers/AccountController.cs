using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenERP.ErpDbContext.DataModel;
using OpenERP.ViewModels;

namespace OpenERP.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<User> _signInManager;

        public AccountController(ILogger<AccountController> logger, SignInManager<User> signInManager)
        {
            this._logger = logger;
            this._signInManager = signInManager;
        }

        public IActionResult Login()
        {
            if(this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "App");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
            
                if(result.Succeeded)
                {
                    if(Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        Redirect(Request.Query["ReturnUrl"].First());
                    }
                    else
                    {
                        return RedirectToAction("Index", "App");
                    }
                }
            
            }
            ModelState.AddModelError("", "User name or password not found");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "App");

        }
    }

}