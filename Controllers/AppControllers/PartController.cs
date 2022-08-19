using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using OpenERP.Data.Repositories;
using OpenERP.ErpDbContext.DataModel;

namespace OpenERP.Controllers.App
{

    [Authorize]
    [Route("app/[controller]")]
    public class PartController : Controller
    {
        private readonly ILogger _logger;
        private readonly UserManager<User> _userManager;
        private readonly IRepository<Part> _partRepository;

        public PartController(ILogger<OpenERP.Controllers.App.PartController> logger, UserManager<User> userManager, IRepository<Part> partRepository)
        {
            this._logger = logger;
            this._userManager = userManager;
            this._partRepository = partRepository;
        }

        [HttpGet]
        public IActionResult Part()
        {
            var companyID = HttpContext.Session.GetObjectFromJson<String>("CurrentCompanyID");
            //above de-serializes so we want this data to persist so needs reset
            HttpContext.Session.SetObjectAsJson("CurrentCompanyID", companyID);

            ViewBag.CurrentCompany = companyID;
            ViewBag.Title = "OpenERP - Part Maintenance";
            return View();
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreatePart(Part part)  //(OpenERP.ViewModels.PartViewModel model)
        {

            if (ModelState.IsValid)
            {
            }
            try
            {
                //can't validate ModelState here as when the POST is called it is invalid, and it is immutable, so adding
                // the required values below doesn't change the IsValid value to true, (even though after the successful
                // assignments, the model will be valid)

                //get company record for CompanyID    
                //part.Company = _context.Companies.Where(c => c.CompanyId.Equals(part.CompanyId)).ToList().FirstOrDefault();
                part.Company = HttpContext.Session.GetObjectFromJson<Company>("CurrentCompany");
                //get UOM record for DefaultUOM string
                //part.Uom = _context.Uoms.Where(u => u.CompanyId.Equals(part.CompanyId) && u.Uom1.Equals(part.DefaultUom)).ToList().FirstOrDefault();

                if (part.Company != null && part.Uom != null)
                {

                    _partRepository.Add(part);
                    _partRepository.SaveChanges();
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