using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;
using SocialMedia.Utilities;
using System.Reflection.Metadata.Ecma335;

namespace SocialMedia.Controllers
{
    public class FriendRequestController : Controller
    {
        private readonly IFriendRequestRepository friendRequestRepository;

        public FriendRequestController(IFriendRequestRepository friendRequestRepository)
        {
            this.friendRequestRepository = friendRequestRepository;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Index(string jwtToken)
        {
            var userId = Helper.ExtractUserIdFromJwt(jwtToken);
            var friendRequests = await friendRequestRepository.GetFriendRequestsAsync(userId); 
            return Ok(friendRequests);
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendRequest(string jwtToken,int reciverId)
        {

            var friendRequest = new FriendRequest
            {
                SenderId = Helper.ExtractUserIdFromJwt(jwtToken),
                ReciverId = reciverId,
                Date = DateTime.Now
            };
           
            var sent =  await friendRequestRepository.SendFriendRequestAsync(friendRequest);

            return Ok(sent);
        }

        [HttpPost("accept")]
        public async Task<IActionResult> AcceptRequest(int senderId,string jwtToken)
        {
            var request = await friendRequestRepository.GetSpecificFriendRequestAsync(senderId, Helper.ExtractUserIdFromJwt(jwtToken));
            if (request == null) return BadRequest();

            await friendRequestRepository.AcceptFriendRequestAsync(request);

            return Ok();
        }

        [HttpPost("reject")]
        public async Task<IActionResult> RejectRequest(int senderId,string jwtToken)
        {
            var request = await friendRequestRepository.GetSpecificFriendRequestAsync(senderId,Helper.ExtractUserIdFromJwt(jwtToken));
            if(request == null) return BadRequest();
            await friendRequestRepository.RejectFriendRequestAsync(request);

            return Ok();
        }


    }
}
