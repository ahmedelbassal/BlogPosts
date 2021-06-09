using AppService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppService.DataContext
{
    public class BlogsDB : DbContext
    {
        public BlogsDB( DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<BlogPost> BlogPosts { get; set; }
        public virtual DbSet<PostComment> PostComments { get; set; }

    }
}
