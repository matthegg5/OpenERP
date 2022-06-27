
using Microsoft.AspNetCore.Mvc;

namespace OpenERP.Controllers
{
    public class AppController : Controller
    {
        public IActionResult Index()
        { 
            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Title = "OpenERP - Contact Administrators";
            return View();
        }

        public IActionResult About()
        {
            ViewBag.Title = "OpenERP - About the project";
            return View();

        }
    }
}
