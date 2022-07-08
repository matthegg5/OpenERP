using OpenERP.ViewModels;
using Microsoft.AspNetCore.Mvc;
using OpenERP.Services;
using OpenERP.ErpDbContext.Models;

namespace OpenERP.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly OpenERPContext _context;

        public AppController(IMailService mailService, OpenERPContext context)
        {
            this._mailService = mailService;
            this._context = context;
        }
        public IActionResult Index()
        { 
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact() //name of IAction needs to match the name of the .cshtml file
        {
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Send email and process
                _mailService.SendMessage("system@openerp.com", model.Subject, $"From: {model.Name} - {model.Email}, Message: {model.Message}");              
                ViewBag.MessageToUser = "Mail sent";
                ModelState.Clear(); //clear the form after submit
            }

            return View();
        }

        public IActionResult About()
        {
            ViewBag.Title = "OpenERP - About the project";
            return View();

        }

        public IActionResult Part()
        {
            var results = _context.Parts
            //.Where(p => p.CompanyId == Session.CompanyID)
            .OrderBy(p => p.PartNum).ToList();
            return View(results);
        }

    }
}
