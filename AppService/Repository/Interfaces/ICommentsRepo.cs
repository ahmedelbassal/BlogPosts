using AppService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppService.Repository.Interfaces
{
    public interface ICommentsRepo
    {

        Task<ActionResult<IEnumerable<PostComment>>> GetPostComments(int BlogId);

        Task<ActionResult<IEnumerable<PostComment>>> GetAll();

        Task<ActionResult<PostComment>> GetOne(int PostId);

        Task<ActionResult<PostComment>> PostOne(PostComment newComment);

        Task<ActionResult<PostComment>> UpdateOne(PostComment UpdatePost);

        Task<int> DeleteOne(int CommentId);
    }
}
