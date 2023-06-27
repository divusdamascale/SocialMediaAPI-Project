using Microsoft.AspNetCore.Mvc;
using SocialMedia.Repositories.Interfaces;
using SocialMedia.Utilities;

namespace SocialMedia.Controllers
{
    public class PostLikeController : Controller
    {
        private readonly IPostLikeRepository repository;

        public PostLikeController(IPostLikeRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost("GiveLike")]

        public async Task<ActionResult> GiveLike(string jwtToken,int postId)
        {
            try
            {
                var userId = Helper.ExtractUserIdFromJwt(jwtToken);

                var like =  await repository.PostLikeAsync(userId, postId);

                if(like == null)
                {
                    return BadRequest();
                }

                return Ok();


            }
            catch (Exception ex) 
            {
                return StatusCode(500,"A apărut o eroare în server. Vă rugăm să încercați din nou mai târziu.");
            }
        }

        [HttpDelete("Unlike")]

        public async Task<ActionResult> Unlike(string jwt, int postId)
        {
            try
            {
                var userId = Helper.ExtractUserIdFromJwt(jwt);

                var result = await repository.Unlike(userId, postId);

                if(result == null)
                {
                    return BadRequest();
                }
                
                return Ok();


            }
            catch(Exception ex) 
            {
                return StatusCode(500,"A apărut o eroare în server. Vă rugăm să încercați din nou mai târziu.");
            }
        }
    }
}
