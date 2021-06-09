using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppService.DataContext;
using AppService.Models;
using AppService.Repository.Interfaces;

namespace AppService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogsRepo _BlogsRepo;

        public BlogsController(IBlogsRepo BlogsRepo)
        {
            _BlogsRepo = BlogsRepo;
        }

        // GET: api/Blogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogPost>>> GetBlogPosts()
        {
            return await _BlogsRepo.GetAll();
        }

        // GET: api/Blogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPost>> GetBlogPost(int id)
        {
            var blogPost = await _BlogsRepo.GetOne(id);

            if (blogPost == null)
            {
                return NotFound();
            }

            return blogPost;
        }

        // PUT: api/Blogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlogPost(int id, BlogPost blogPost)
        {
            if (id != blogPost.Id)
            {
                return BadRequest();
            }


            try
            {
                var response= await _BlogsRepo.UpdateOne(blogPost);

                if (response == null) return NotFound();
            }
            catch (DbUpdateConcurrencyException)
            {
               
                    return StatusCode(500);
            }

            return Ok(blogPost);
        }

        // POST: api/Blogs
        [HttpPost]
        public async Task<ActionResult<BlogPost>> PostBlogPost(BlogPost blogPost)
        {

            await _BlogsRepo.PostOne(blogPost);

            return CreatedAtAction("GetBlogPost", new { id = blogPost.Id }, blogPost);
        }

        // DELETE: api/Blogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogPost(int id)
        {
            var numOfDeleted = await _BlogsRepo.DeleteOne(id);
            if (numOfDeleted < 1)
            {
                return NotFound();
            }


            return NoContent();
        }

    }
}
