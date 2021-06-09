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
    public class BlogRepo : IBlogsRepo
    {
        public readonly BlogsDB _db;

        public BlogRepo(BlogsDB db)
        {
            _db = db;
        }

        public async Task<ActionResult<IEnumerable<BlogPost>>> GetAll()
        {
            return await _db.BlogPosts.Include(ww=>ww.Comments).ToListAsync();
        }

        public async Task<ActionResult<BlogPost>> GetOne(int Id)
        {
            return await _db.BlogPosts.Include(ww=>ww.Comments).FirstOrDefaultAsync(ww=>ww.Id == Id);
        }

        public async Task<ActionResult<BlogPost>> PostOne(BlogPost newPost)
        {
            newPost.CreatedAt =newPost.ModifiedAt= DateTime.Now;

            await _db.BlogPosts.AddAsync(newPost);

            await _db.SaveChangesAsync();

            return newPost;
        }

        public async Task<ActionResult<BlogPost>> UpdateOne(BlogPost UpdatePost)
        {
            // check if post exists
            BlogPost checkPost=await _db.BlogPosts.FindAsync(UpdatePost.Id);

            if (checkPost == null) return null;

            UpdatePost.ModifiedAt = DateTime.Now;

            checkPost.Content = UpdatePost.Content;
            checkPost.Title = UpdatePost.Title;
            checkPost.ModifiedAt = DateTime.Now;

            await _db.SaveChangesAsync();

            return UpdatePost;
        }


        public async Task<int> DeleteOne(int Id)
        {
            // check if post exist
            BlogPost deletePost = await _db.BlogPosts.FindAsync(Id);

            // check if it has comments to avoid foreignkey exception error
            if (deletePost != null)
            {
                var postComments = await _db.PostComments.Where(ww => ww.PostId == Id).ToListAsync();

                if (postComments.Count > 0)
                {
                    _db.PostComments.RemoveRange(postComments);
                    await _db.SaveChangesAsync();
                }

                _db.BlogPosts.Remove(deletePost);
            }


            return await _db.SaveChangesAsync();

        }


    }
}
