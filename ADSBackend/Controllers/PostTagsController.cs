using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ADSBackend.Data;
using ADSBackend.Models;

namespace ADSBackend.Controllers
{
    public class PostTagsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostTagsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PostTags
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PostTag.Include(p => p.Post).Include(p => p.Tag);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PostTags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postTag = await _context.PostTag
                .Include(p => p.Post)
                .Include(p => p.Tag)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (postTag == null)
            {
                return NotFound();
            }

            return View(postTag);
        }

        // GET: PostTags/Create
        public IActionResult Create()
        {
            ViewData["PostId"] = new SelectList(_context.Post, "PostId", "PostId");
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Id");
            return View();
        }

        // POST: PostTags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,TagId")] PostTag postTag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postTag);
                _context.Post.Find(postTag.PostId).Tags.Add(postTag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PostId"] = new SelectList(_context.Post, "PostId", "PostId", postTag.PostId);
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Id", postTag.TagId);
            return View(postTag);
        }

        // GET: PostTags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postTag = await _context.PostTag.FindAsync(id);
            if (postTag == null)
            {
                return NotFound();
            }
            ViewData["PostId"] = new SelectList(_context.Post, "PostId", "PostId", postTag.PostId);
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Id", postTag.TagId);
            return View(postTag);
        }

        // POST: PostTags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,TagId")] PostTag postTag)
        {
            if (id != postTag.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postTag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostTagExists(postTag.PostId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PostId"] = new SelectList(_context.Post, "PostId", "PostId", postTag.PostId);
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Id", postTag.TagId);
            return View(postTag);
        }

        // GET: PostTags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postTag = await _context.PostTag
                .Include(p => p.Post)
                .Include(p => p.Tag)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (postTag == null)
            {
                return NotFound();
            }

            return View(postTag);
        }

        // POST: PostTags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postTag = await _context.PostTag.FindAsync(id);
            _context.PostTag.Remove(postTag);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostTagExists(int id)
        {
            return _context.PostTag.Any(e => e.PostId == id);
        }
    }
}
