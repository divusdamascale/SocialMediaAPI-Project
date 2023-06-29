using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using SocialMedia.Models.DTO;
using SocialMedia.Repositories;
using SocialMedia.Repositories.Interfaces;
using SocialMedia.Utilities;
using System.Runtime.CompilerServices;

namespace SocialMedia.Controllers
{
    public class PostCommentController : Controller
    {
        private readonly IPostCommentRepository repo;

        public PostCommentController(IPostCommentRepository repo)
        {
            this.repo = repo;
        }

        [HttpPost("GiveComment")]

        public async Task<IActionResult> Comment([FromBody]CommentDTO comment , int postId, string Jwt)
        {
            try
            {

                var userId = Helper.ExtractUserIdFromJwt(Jwt);
                await repo.AddComment(comment,postId,userId);

                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500,"A apărut o eroare în server. Vă rugăm să încercați din nou mai târziu.");
            }
        }

        [HttpDelete("DeleteComment")]

        public async Task<IActionResult> DeleteComment(int commentId)
        {
            try
            {
                await repo.DeleteComment(commentId);
                return Ok();
            }
            catch(Exception ex) 
            {
                return StatusCode(500,"A apărut o eroare în server. Vă rugăm să încercați din nou mai târziu.");
            }
        }

    }
}
