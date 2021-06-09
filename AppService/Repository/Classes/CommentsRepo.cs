using AppService.DataContext;
using AppService.Models;
using AppService.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppService.Repository.Classes
{
    public class CommentsRepo : ICommentsRepo
    {
        public BlogsDB _db { get; set; }

        public CommentsRepo(BlogsDB db)
        {
            _db = db;
        }


        public async Task<ActionResult<IEnumerable<PostComment>>> GetPostComments(int BlogId)
        {
            return await _db.PostComments.Where(ww => ww.PostId == BlogId).ToListAsync();
        }


        public async Task<ActionResult<IEnumerable<PostComment>>> GetAll()
        {
            return await _db.PostComments.ToListAsync();
        }


        public async Task<ActionResult<PostComment>> GetOne(int CommentId)
        {
            return await _db.PostComments.FindAsync(CommentId);
        }

        public async Task<ActionResult<PostComment>> PostOne(PostComment newComment)
        {
            newComment.CreatedAt = newComment.ModifiedAt = DateTime.Now;

            await _db.PostComments.AddAsync(newComment);

            await _db.SaveChangesAsync();

            return newComment;
        }

        public async Task<ActionResult<PostComment>> UpdateOne(PostComment UpdatePost)
        {
            // check if post exists
            var PostExist =await _db.PostComments.FindAsync(UpdatePost.Id);

            if (PostExist == null) return null;

            PostExist.Content = UpdatePost.Content;
            PostExist.ModifiedAt = DateTime.Now;

            await _db.SaveChangesAsync();

            return UpdatePost;
        }


        public async Task<int> DeleteOne(int CommentId)
        {
            PostComment deleteComm =await _db.PostComments.FindAsync(CommentId);

            // check if it has comments to avoid foreignkey exception error
            if (deleteComm != null)
            {
                _db.PostComments.Remove(deleteComm);
            }

            return await _db.SaveChangesAsync();

        }

    }
}
