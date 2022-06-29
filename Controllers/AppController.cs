using OpenERP.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace OpenERP.Controllers
{
    public class AppController : Controller
    {
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
                

            }
            else 
            {
                //throw new InvalidProgramException("Model is invalid");
            }

            return View();
        }

        public IActionResult About()
        {
            ViewBag.Title = "OpenERP - About the project";
            return View();

        }
    }
}
