using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ADSBackend.Controllers
{
    public class ExploreController : Controller
    {
        public IActionResult Index(string s = "")
        {
            string testDebug = s;
            return View();
        }
    }
}
