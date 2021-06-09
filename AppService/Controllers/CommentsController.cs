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
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsRepo _db;

        public CommentsController(ICommentsRepo db)
        {
            _db = db;
        }

        // GET: api/Comments/post/1
        // Get comments of specific blog
        [HttpGet("post/{id}")]
        public async Task<ActionResult<IEnumerable<PostComment>>> GetPostComments(int id)
        {
            return await _db.GetPostComments(id);
        }


        // GET: api/Comments/post/1
        // get all comments of all blogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostComment>>> GetAllBlogsComments()
        {
            return await _db.GetAll();
        }


        // GET: api/Comments/5
        // get specific comment by only its id in database
        [HttpGet("{id}")]
        public async Task<ActionResult<PostComment>> GetCommentById(int id)
        {
            var postComment = await _db.GetOne(id);

            if (postComment == null)
            {
                return NotFound();
            }

            return postComment;
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPostComment(int id, PostComment postComment)
        {
            if (id != postComment.Id)
            {
                return BadRequest();
            }


            try
            {
               var postUpdated= await _db.UpdateOne(postComment);

                if (postUpdated == null) return NotFound();
            }
            catch (DbUpdateConcurrencyException)
            {
                
                    return StatusCode(500);
            }

            return Ok(postComment);
        }

        // POST: api/Comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PostComment>> PostPostComment(PostComment postComment)
        {
            await _db.PostOne(postComment);

            //return CreatedAtAction("GetPostComment", new { id = postComment.Id }, postComment);
            return Ok(postComment);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostComment(int id)
        {
            var postComment = await _db.DeleteOne(id);
            if (postComment < 1)
            {
                return NotFound();
            }

            return Ok("Deleted");
        }

    }
}
