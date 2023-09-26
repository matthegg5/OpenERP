using OpenERP.ViewModels;
using Microsoft.AspNetCore.Mvc;
using OpenERP.Services;
using OpenERP.ErpDbContext.DataModel;
using Microsoft.AspNetCore.Authorization;

namespace OpenERP.Controllers.App
{
    public class AppController : Controller
    {


        public AppController()
        {
        }
        public async Task<IActionResult> Index()
        { 
            var companyID = HttpContext.Session.GetString("CurrentCompanyID");
            if (!String.IsNullOrEmpty(companyID))
            {
                HttpContext.Session.SetString("CurrentCompanyID", companyID);
            }
            return View();
        }

        [HttpGet("contact")]
        public async Task<ActionResult> Contact() //name of IAction needs to match the name of the .cshtml file
        {
            return View();
        }

        [HttpPost("contact")]
        public async Task<IActionResult> Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Send email and process
                //_mailService.SendMessage("system@openerp.com", model.Subject, $"From: {model.Name} - {model.Email}, Message: {model.Message}");              
                ViewBag.MessageToUser = "Mail sent";
                ModelState.Clear(); //clear the form after submit
            }

            return View();
        }

        public async Task<ActionResult> About()
        {
            ViewBag.Title = "OpenERP - About the project";
            return View();

        }

    }
}
