﻿using Microsoft.AspNetCore.Mvc;

namespace ADSBackend.Controllers
{
    public class HomeController : Controller
    {
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
                    return RedirectToAction("Index", "User");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
    }
}
