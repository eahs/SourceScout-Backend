using Microsoft.AspNetCore.Mvc;
using System;
namespace ADSBackend.Controllers
{
    public class HomeController : Controller
    {
        
        //public string SearchInput { get; set; }

        public IActionResult Index()
        {
           
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        public IActionResult Search()
        {
            return RedirectToAction("Index", "Explore", new { s = "queryStringValue1" });
        }
    }
}
