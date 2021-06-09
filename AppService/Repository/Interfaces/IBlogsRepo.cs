using AppService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppService.Repository.Interfaces
{
    public interface IBlogsRepo
    {
        Task<ActionResult<IEnumerable<BlogPost>>> GetAll();

        Task<ActionResult<BlogPost>> GetOne(int Id);

        Task<ActionResult<BlogPost>> PostOne(BlogPost newPost);

        Task<ActionResult<BlogPost>> UpdateOne(BlogPost UpdatePost);

        Task<int> DeleteOne(int Id);

    }
}
