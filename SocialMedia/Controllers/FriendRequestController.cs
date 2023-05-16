using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;
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
        public async Task<IActionResult> Index(int userId)
        {
            //De implementat sa poate fi folosit doar cu JWT VALID si scoaterea userID din jwt
            var friendRequests = await friendRequestRepository.GetFriendRequestsAsync(userId); 
            return Ok(friendRequests);
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendRequest(int senderId,int reciverId)
        {
            //De implementat sa poate fi folosit doar cu JWT VALID si scoaterea senderId din jwt
            //De implementat verificarea daca sunt deja prieteni
            var friendRequest = new FriendRequest
            {
                SenderId = senderId,
                ReciverId = reciverId,
                Date = DateTime.Now
            };
           
            var sent =  await friendRequestRepository.SendFriendRequestAsync(friendRequest);

            return Ok(sent);
        }

        [HttpPost("accept")]
        public async Task<IActionResult> AcceptRequest(int senderId,int reciverId)
        {
            var request = await friendRequestRepository.GetSpecificFriendRequestAsync(senderId, reciverId);
            if (request == null) return BadRequest();

            await friendRequestRepository.AcceptFriendRequestAsync(request);

            return Ok();
        }

        [HttpPost("reject")]
        public async Task<IActionResult> RejectRequest(int senderId,int reciverId)
        {
            var request = await friendRequestRepository.GetSpecificFriendRequestAsync(senderId,reciverId);
            if(request == null) return BadRequest();
            await friendRequestRepository.RejectFriendRequestAsync(request);

            return Ok();
        }
    }
}
