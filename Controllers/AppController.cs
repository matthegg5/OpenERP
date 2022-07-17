using OpenERP.ViewModels;
using Microsoft.AspNetCore.Mvc;
using OpenERP.Services;
<<<<<<< HEAD
using OpenERP.ErpDbContext.Models;
=======
using OpenERP.ErpDbContext.DataModel;
>>>>>>> 5fd5afd (	new file:   Controllers/AppController.cs)
using Microsoft.AspNetCore.Authorization;

namespace OpenERP.Controllers
{
    public class AppController : Controller
    {
<<<<<<< HEAD
        private readonly IMailService _mailService;
        private readonly OpenERPContext _context;

        public AppController(IMailService mailService, OpenERPContext context)
        {
            this._mailService = mailService;
=======

        private readonly OpenERPContext _context;

        public AppController(OpenERPContext context)
        {

>>>>>>> 5fd5afd (	new file:   Controllers/AppController.cs)
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
<<<<<<< HEAD
                _mailService.SendMessage("system@openerp.com", model.Subject, $"From: {model.Name} - {model.Email}, Message: {model.Message}");              
=======
                //_mailService.SendMessage("system@openerp.com", model.Subject, $"From: {model.Name} - {model.Email}, Message: {model.Message}");              
>>>>>>> 5fd5afd (	new file:   Controllers/AppController.cs)
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

        [Authorize]
        public IActionResult Part()
        {
            var results = _context.Parts
            //.Where(p => p.CompanyId == Session.CompanyID)
            .OrderBy(p => p.PartNum).ToList();
            return View(results);
        }

    }
}
