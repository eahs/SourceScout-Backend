using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ADSBackend.Data;
using ADSBackend.Models;
using ADSBackend.Controllers.Api.v1;
using System.Net;

namespace ADSBackend.Controllers.Api.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/v1/Post
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPost()
        {
            var posts = await _context.Post.Include(p => p.Tags)
                                         .ThenInclude(t => t.Tag)
                                         .Include(p => p.Category)
                                         .ToListAsync();
            return posts;
        }

        
        [HttpGet("Search/Tags")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPost([FromQuery]string[] Tags)
        {
            List<PostTag> postTags = new List<PostTag>();
            //adds all post tags that have a tag in the array Tags. All post tags are then stored in the list postTags
            foreach (string t in Tags)
            {
                var postTagsTemp = await _context.PostTag.Include(pt => pt.Post).Include(pt => pt.Tag)
                                              .Where(pt => t.Equals(pt.Tag.TagName))
                                              .OrderBy(pt => pt.Post.Score)
                                              .ToListAsync();
                foreach (PostTag pt in postTagsTemp)
                {
                    postTags.Add(pt);
                }
            }
            
            HashSet<int> idsOfPosts = new HashSet<int>();
            List<int> idsOfRequestedPosts = new List<int>();
            //adds all ids from the posttags. all uniqe because its a Hashset
            foreach (PostTag pt in postTags)
            {
                idsOfPosts.Add(pt.PostId);
            }
            List<List<PostTag>> groupsOfPosts = new List<List<PostTag>>();
            //seperates post tags into seperate lists based on the ids of the post. That means each list is one post with all of its tags
            foreach(int id in idsOfPosts)
            {
                groupsOfPosts.Add(postTags.Where(pt => pt.PostId == id).ToList());
            }
            //for each of the groups of posts if the number of post tags is number to the tags specified by the query string, 
            //then that post id must have all the tags and/or more. We then add that id to a list
            foreach(List<PostTag> li in groupsOfPosts)
            {
                if (li.Count == Tags.Length)
                {
                    idsOfRequestedPosts.Add(li[0].PostId);
                }
            }
            
            
            //List of all posts that have an id that are in the list of ids that have the correct tags
            List<Post> posts = await _context.Post.Include(p => p.Tags)
                                                  .ThenInclude(pt => pt.Tag)
                                                  .Include(p => p.Category)
                                                  .Where(p => idsOfRequestedPosts.Contains(p.PostId))
                                                  .ToListAsync();
            return posts;
        }

        [HttpGet("Search/Category")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPost([FromQuery] string Category)
        {
            return await _context.Post.Include(p => p.Category)
                                      .Include(p => p.Tags)
                                      .ThenInclude(pt => pt.Tag)
                                      .Where(p => p.Category.Name.Equals(Category))
                                      .ToListAsync();
        }


        // GET: api/v1/Post/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = await _context.Post.Include(p => p.Tags)
                                          .ThenInclude(t => t.Tag)
                                          .Include(p => p.Category)
                                          .FirstOrDefaultAsync(p => p.PostId == id);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        // PUT: api/v1/Post/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, Post post)
        {
            if (id != post.PostId)
            {
                return BadRequest();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/v1/Post
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Post>> PostPost(Post post)
        {
            post.Tags = new List<PostTag>();
            foreach (String name in post.TagNames)
            {
                Tag? TheTag = await _context.Tag.FirstOrDefaultAsync(t => t.TagName.Contains(name));
                if (TheTag == null)
                {
                    _context.Add(new Tag { TagName = name });
                    TheTag = await _context.Tag.FirstOrDefaultAsync(t => t.TagName.Contains(name));
                }
                PostTag pt = new PostTag
                {
                    Post = post,
                    PostId = post.PostId,
                    Tag = TheTag ?? new Tag { TagName = "Error" }
                };
                pt.TagId = pt.Tag.Id;
                post.Tags.Add(pt);
            }
            
            _context.Post.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPost), new { id = post.PostId }, post);
        }

        // DELETE: api/v1/Post/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Post>> DeletePost(int id)
        {
            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Post.Remove(post);
            await _context.SaveChangesAsync();

            return post;
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.PostId == id);
        }
    }
}
