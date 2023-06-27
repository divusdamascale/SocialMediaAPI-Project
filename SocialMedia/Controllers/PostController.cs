using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;
using SocialMedia.Models.DTO;
using SocialMedia.Repositories.Interfaces;
using SocialMedia.Utilities;
using System.Collections;
using System.Runtime.CompilerServices;

namespace SocialMedia.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository postRepository;

        public PostController(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        [HttpGet("getMyPosts")]
        public async Task<ActionResult<IEnumerable<MyPostDTO>>> GetMyPost(string jwtToken)
        {
            try
            {
                var userId = Helper.ExtractUserIdFromJwt(jwtToken);
                var posts = await postRepository.GetMyPosts(userId);
                return Ok(posts);
            }
            catch(Exception ex)
            {

                return StatusCode(500,"A apărut o eroare în server. Vă rugăm să încercați din nou mai târziu.");
            }
        }

        [HttpGet("getXpost")]

        public async Task<ActionResult<IEnumerable<FriendPostDTO>>> GetXPosts(int UserId)
        {
            try {
                var posts = await postRepository.GetxPosts(UserId);



                return Ok(posts);
            }
            catch(Exception ex) 
            {
                return StatusCode(500,"A apărut o eroare în server. Vă rugăm să încercați din nou mai târziu.");
            }
        }


        [HttpGet("friendsPosts")]

        public async Task<ActionResult<IEnumerable<FriendPostDTO>>> GetFriendsPosts(string jwtToken)
        {
            try
            {
                var userId = Helper.ExtractUserIdFromJwt(jwtToken);
                var posts = await postRepository.GetFriendsPost(userId);
                return Ok(posts);
            }
            catch(Exception ex)
            {
                return StatusCode(500,"A apărut o eroare în server. Vă rugăm să încercați din nou mai târziu.");
            }
        }


        [HttpPost("createPost")]

        public async Task<ActionResult<CreatePostDTO>> CreatePost([FromBody] CreatePostDTO post,string jwtToken)
        {
            try
            {
                var userId = Helper.ExtractUserIdFromJwt(jwtToken);

                var postN = new Post
                {
                    UserId = userId,
                    Content = post.Content,
                    CreatedAt = DateTime.UtcNow,
                };

                await postRepository.CreatePost(postN);

                return Ok(post);
            }
            catch(Exception ex)
            {

                return StatusCode(500,"A apărut o eroare în server. Vă rugăm să încercați din nou mai târziu.");
            }
        }

        [HttpGet("ViewLikes")]
        public async Task<IEnumerable<ViewLikeDTO>> GetPostLikes(int postId)
        {
            var likeList = await postRepository.ViewLikes(postId);

            return likeList;
        }
        

    }
}
