using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ADSBackend.Data;
using ADSBackend.Models;
using Google.Apis.YouTube.v3;

namespace ADSBackend.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Post
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Post.Include(p => p.Category).Include(p => p.Member);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Post/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .Include(p => p.Category)
                .Include(p => p.Member)
                .Include(p => p.Tags)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Post/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name");
            ViewData["MemberId"] = new SelectList(_context.Member, "MemberId", "Nickname");
            return View();
        }

        // POST: Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,Title,MemberId,UpVotes,DownVotes,Score,Thumbnail,Link,Description,DateCreated,DateEdited,Deleted,CategoryId")] Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryId", post.CategoryId);
            ViewData["MemberId"] = new SelectList(_context.Member, "MemberId", "Email", post.MemberId);
            return View(post);
        }

        // GET: Post/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryId", post.CategoryId);
            ViewData["MemberId"] = new SelectList(_context.Member, "MemberId", "Email", post.MemberId);
            return View(post);
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,Title,MemberId,UpVotes,DownVotes,Score,Thumbnail,Link,Description,DateCreated,DateEdited,Deleted,CategoryId")] Post post)
        {
            if (id != post.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.PostId))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryId", post.CategoryId);
            ViewData["MemberId"] = new SelectList(_context.Member, "MemberId", "Email", post.MemberId);
            return View(post);
        }

        // GET: Post/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .Include(p => p.Category)
                .Include(p => p.Member)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Post.Include(p => p.Tags)
                                          .ThenInclude(t => t.Tag)
                                          .FirstOrDefaultAsync(p => p.PostId == id);
            foreach (PostTag pt in post.Tags)
            {
                _context.PostTag.Remove(pt);
            }
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.PostId == id);
        }
        public async Task<IActionResult> View(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            if (post.Link.Contains("&"))
            {
                post.Link = post.Link.Substring(0, post.Link.IndexOf("&"));
            }
            if (post.Link.Contains("watch"))
            {
                string finalUrl = post.Link.Substring(0, post.Link.IndexOf("watch"));
                finalUrl += "embed/" + post.Link.Substring(post.Link.IndexOf("watch") + 8);
                post.Link = finalUrl;
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryId", post.CategoryId);
            ViewData["MemberId"] = new SelectList(_context.Member, "MemberId", "Email", post.MemberId);
            return View(post);
        }
    }
}
