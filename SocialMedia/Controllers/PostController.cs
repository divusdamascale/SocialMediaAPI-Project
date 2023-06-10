using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;
using SocialMedia.Models.DTO;
using SocialMedia.Repositories.Interfaces;
using SocialMedia.Utilities;
using System.Collections;

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
            var userId = Helper.ExtractUserIdFromJwt(jwtToken);

            var posts = await postRepository.GetMyPosts(userId);

            return Ok(posts);
        }

        [HttpGet("friendsPosts")]

        public async Task<ActionResult<IEnumerable<FriendPostDTO>>> GetFriendsPosts(string jwtToken)
        {
            var userId = Helper.ExtractUserIdFromJwt(jwtToken);

            var posts = await postRepository.GetFriendsPost(userId);

            return Ok(posts);

        }

        [HttpPost("createPost")]

        public async Task<ActionResult<CreatePostDTO>> CreatePost([FromBody]CreatePostDTO post,string jwtToken)
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
        
        


    }
}
