using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using OpenERP.Infrastructure;
using OpenERP.ErpDbContext.DataModel;

namespace OpenERP.Controllers.App
{

    [Authorize]
    [Route("app/[controller]")]
    public class PartController : Controller
    {
        private readonly ILogger _logger;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork unitOfWork;

        public PartController(ILogger<OpenERP.Controllers.App.PartController> logger,
                                UserManager<User> userManager, 
                                IUnitOfWork unitOfWork)
        {
            this._logger = logger;
            this._userManager = userManager;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult> Part()
        {
            //var companyID = HttpContext.Session.GetObjectFromJson<String>("CurrentCompanyID");
            //above de-serializes so we want this data to persist so needs reset

            var companyID = HttpContext.Session.GetString("CurrentCompanyID");

            ViewBag.CurrentCompany = companyID;
            ViewBag.Title = "OpenERP - Part Maintenance";
            //HttpContext.Session.SetObjectAsJson("CurrentCompanyID", companyID);
            return View();
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreatePart(OpenERP.ViewModels.PartViewModel model)
        {
            try
            {
                //can't validate ModelState here as when the POST is called it is invalid, and it is immutable, so adding
                // the required values below doesn't change the IsValid value to true, (even though after the successful
                // assignments, the model will be valid)

                var newPart = new ErpDbContext.DataModel.Part();

                //get company record for CompanyID    
                //part.Company = _context.Companies.Where(c => c.CompanyId.Equals(part.CompanyId)).ToList().FirstOrDefault();

                HttpContext.Session.SetObjectAsJson("CurrentCompanyID", model.CompanyId);

                newPart.Company = unitOfWork.CompanyRepository.GetByID(model.CompanyId);
                //get UOM record for DefaultUOM string
                newPart.Uom = unitOfWork.UomRepository.GetByID(model.CompanyId, model.DefaultUOM);

                if (newPart.Company != null && newPart.Uom != null)
                {
                    newPart.PartNum = model.PartNum;
                    newPart.PartDescription = model.PartDescription;

                    unitOfWork.PartRepository.Add(newPart);
                    await unitOfWork.SaveChangesAsync();
                    return RedirectToAction("Part", "App");
                }
                else
                {
                    return BadRequest($"Failed to create Part {model.PartNum} - Company or UOM value invalid");
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