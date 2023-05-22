using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Repositories;
using SocialMedia.Repositories.Interfaces;
using SocialMedia.Utilities;

namespace SocialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendshipController : ControllerBase
    {
        private readonly IFriendshipRepository friendshipRepository;

        public FriendshipController(IFriendshipRepository friendshipRepository)
        {
            this.friendshipRepository = friendshipRepository;
        }

        [HttpGet("friends")]
        public async Task<IActionResult> Friends(string jwtToken)
        {
            var userId = Helper.ExtractUserIdFromJwt(jwtToken);
            var friendRequests = await friendshipRepository.GetFriendsAsync(userId);
            return Ok(friendRequests);
        }

        [HttpDelete("deleteFriend")]

        public async Task<IActionResult> DeleteFriend(string jwtToken,int deleteFriendId)
        {
            var userId = Helper.ExtractUserIdFromJwt(jwtToken);
            await friendshipRepository.DeleteFriend(userId,deleteFriendId);

            return Ok();
        }

    }
}
