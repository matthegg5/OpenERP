using OpenERP.ViewModels;
using Microsoft.AspNetCore.Mvc;
using OpenERP.Services;
using OpenERP.ErpDbContext.DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

namespace OpenERP.Controllers.App
{

    [Authorize]
    [Route("app/[controller]")]
    public class PartController : Controller
    {
        private readonly OpenERPContext _context;
        private readonly ILogger _logger;
        private readonly UserManager<User> _userManager;

        public PartController(OpenERPContext context, ILogger<OpenERP.Controllers.App.PartController> logger, UserManager<User> userManager)
        {
            this._context = context;
            this._logger = logger;
            this._userManager = userManager;
        }

        [HttpGet]
        public IActionResult Part()
        {
            ViewBag.Title = "OpenERP - Part Maintennce";
            return View();

        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreatePart(Part part)
        {

            if(ModelState.IsValid)
            {
            }
            try
            {
                //can't validate ModelState here as when the POST is called it is invalid, and it is immutable, so adding
                // the required values below doesn't change the IsValid value to true, (even though after the successful
                // assignments, the model will be valid)
                
                //get company record for CompanyID    
                part.Company = _context.Companies.Where(c => c.CompanyId.Equals(part.CompanyId)).ToList().FirstOrDefault();
                //get UOM record for DefaultUOM string
                part.Uom = _context.Uoms.Where(u => u.CompanyId.Equals(part.CompanyId) && u.Uom1.Equals(part.DefaultUom)).ToList().FirstOrDefault();

                if(part.Company != null && part.Uom != null)
                {
                    _context.Parts.Add(part);
                    _context.SaveChanges();
                    return RedirectToAction("Part", "App");
                }
                else
                {
                    return BadRequest($"Failed to create Part {part.PartNum} - Company or UOM value invalid");
                }


            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create Part: {ex.ToString()}");
                return BadRequest($"Failed to create Part: {ex.ToString()}");
            }

        }
    }

}