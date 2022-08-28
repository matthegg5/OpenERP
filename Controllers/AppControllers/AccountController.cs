using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenERP.Data.Repositories;
using OpenERP.ErpDbContext.DataModel;
using OpenERP.ViewModels;

namespace OpenERP.Controllers.App
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        private readonly IRepository<Company> _companyRepository;

        public AccountController(ILogger<AccountController> logger,
                SignInManager<User> signInManager,
                UserManager<User> userManager,
                IConfiguration config,
                IRepository<Company> companyRepository)
        {
            this._logger = logger;
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._config = config;
            this._companyRepository = companyRepository;
        }

        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "App");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false).ConfigureAwait(false);

                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        //Redirect(Request.Query["ReturnUrl"].First());
                        return RedirectToAction("SessionProperties", "Account", "", Request.Query["ReturnUrl"].First());
                    }
                    else
                    {
                        return RedirectToAction("SessionProperties", "Account");
                    }
                }

            }
            ModelState.AddModelError("", "User name or password not found");
            return View();
        }

        [HttpGet]
        public IActionResult SessionProperties()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SessionProperties(SessionPropertiesViewModel model)
        {

            if (ModelState.IsValid)
            {
                if(_companyRepository.GetByID(model.CurrentCompanyID) == null)
                {
                    //throw new InvalidDataException("Company record not found");
                    ModelState.AddModelError("", $"Invalid Company ID - {model.CurrentCompanyID}");
                    _logger.LogError($"Invalid Company ID - {model.CurrentCompanyID}");
                    return View();
                }

                
                //sets the company for the current session and stores it as a Json record 
                //HttpContext.Session.SetObjectAsJson("CurrentCompanyID", model.CurrentCompanyID);

                HttpContext.Session.SetString("CurrentCompanyID", model.CurrentCompanyID);

                if (Request.Query.Keys.Contains("ReturnUrl"))
                {
                    Redirect(Request.Query["ReturnUrl"].First());
                }
                else
                {
                    return RedirectToAction("Index", "App");
                }
                
            }


            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync().ConfigureAwait(false);
            return RedirectToAction("Index", "App");

        }


        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = await _userManager.FindByNameAsync(model.Username).ConfigureAwait(false);

                if (user != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false).ConfigureAwait(false);

                    if (result.Succeeded)
                    {
                        //Create JWT token for API authentication
                        var claims = new[]
                        {
                          new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                          new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

                        var token = new JwtSecurityToken(
                            _config["Tokens:Issuer"],
                            _config["Tokens:Audience"],
                            claims,
                            signingCredentials: creds,
                            expires: DateTime.UtcNow.AddMinutes(20));

                        return Created("", new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        });
                    }
                }
            }

            return BadRequest();
        }
    }

}