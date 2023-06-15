using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
            try
            {
                var userId = Helper.ExtractUserIdFromJwt(jwtToken);
                var friendRequests = await friendRequestRepository.GetFriendRequestsAsync(userId);
                return Ok(friendRequests);
            }
            catch(Exception ex)
            {

                return StatusCode(500,"A apărut o eroare în server. Vă rugăm să încercați din nou mai târziu.");
            }
        }


        [HttpPost("send")]
        public async Task<IActionResult> SendRequest(string jwtToken,int reciverId)
        {
            try
            {
                var friendRequest = new FriendRequest
                {
                    SenderId = Helper.ExtractUserIdFromJwt(jwtToken),
                    ReciverId = reciverId,
                    Date = DateTime.Now
                };

                var sent = await friendRequestRepository.SendFriendRequestAsync(friendRequest);

                return Ok(sent);
            }
            catch(Exception ex)
            {
                return StatusCode(500,"A apărut o eroare în server. Vă rugăm să încercați din nou mai târziu.");
            }
        }

        [HttpPost("accept")]
        public async Task<IActionResult> AcceptRequest(int senderId,string jwtToken)
        {
            try
            {
                var request = await friendRequestRepository.GetSpecificFriendRequestAsync(senderId,Helper.ExtractUserIdFromJwt(jwtToken));
                if(request == null)
                {
                    return BadRequest();
                }

                await friendRequestRepository.AcceptFriendRequestAsync(request);

                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500,"A apărut o eroare în server. Vă rugăm să încercați din nou mai târziu.");
            }
        }


        [HttpPost("reject")]
        public async Task<IActionResult> RejectRequest(int senderId,string jwtToken)
        {
            try
            {
                var request = await friendRequestRepository.GetSpecificFriendRequestAsync(senderId,Helper.ExtractUserIdFromJwt(jwtToken));
                if(request == null)
                    return BadRequest();

                await friendRequestRepository.RejectFriendRequestAsync(request);

                return Ok();
            }
            catch(Exception ex)
            {

                return StatusCode(500,"A apărut o eroare în server. Vă rugăm să încercați din nou mai târziu.");
            }
        }



    }
}
