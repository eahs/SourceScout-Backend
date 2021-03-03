using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ADSBackend.Models;
using ADSBackend.Data;

namespace ADSBackend.Controllers
{
    
    public class ExploreController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExploreController(ApplicationDbContext context)
        {
            _context = context;
        }

        List<Post> queryResult = new List<Post>();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Search(string query = "")
        {
            
            query = query.Trim();
            if (query == "")
            {
                queryResult = _context.Post.ToList();
            }
            else
            {
                queryResult = _context.Post.Where(p => p.Title.ToLower().Contains(query.ToLower())).ToList();
                string d = query.ToLower();
            }
            ViewData["Videos"] = queryResult;
            return View();
        }

    }
}
