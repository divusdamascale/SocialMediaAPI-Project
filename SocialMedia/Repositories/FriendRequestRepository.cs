using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using SocialMedia.Models;
using SocialMedia.Models.DTO;
using SocialMedia.Repositories.Interfaces;

namespace SocialMedia.Repositories

{
    public class FriendRequestRepository : IFriendRequestRepository
    {
        private readonly SocialDBContext _dBContext;

        public FriendRequestRepository(SocialDBContext dBContext)
        {
            this._dBContext = dBContext;
        }

        public async Task<Friendship> AcceptFriendRequestAsync(FriendRequest request)
        {
            var friendship = new Friendship
            {
                User1Id = request.ReciverId,
                User2Id = request.SenderId,
                Date = DateTime.Now,
            };

            var friendshipReverse = new Friendship
            {
                User1Id = request.SenderId,
                User2Id = request.ReciverId,
                Date = DateTime.Now,
            };


            await _dBContext.Friendships.AddAsync(friendship);
            await _dBContext.Friendships.AddAsync(friendshipReverse);
            _dBContext.FriendRequests.Remove(request);
            await _dBContext.SaveChangesAsync();
            //not the best practice(to remediate)
            return friendship;
        }

        public async Task<List<FriendRequestDTO>> GetFriendRequestsAsync(int userId)
        {
            var friends = await _dBContext.FriendRequests
                .Include(fr => fr.Sender)
                .Where(fr => fr.ReciverId == userId)
                .Select(fr => new FriendRequestDTO
                {   ReciverId = fr.ReciverId,
                    SenderId = fr.SenderId,
                    SenderName = fr.Sender.Name,
                    SenderEmail = fr.Sender.Email,

                })
                .ToListAsync<FriendRequestDTO>();

            return friends;
        }

        public async Task<FriendRequest> GetSpecificFriendRequestAsync(int senderId,int reciverId)
        {
            return await _dBContext.FriendRequests.SingleOrDefaultAsync(fr => fr.SenderId == senderId && fr.ReciverId == reciverId); 
        }

        public async Task RejectFriendRequestAsync(FriendRequest request)
        {
            _dBContext.FriendRequests.Remove(request);
            await _dBContext.SaveChangesAsync();
        }

        public async Task<SendFriendRequestDTO> SendFriendRequestAsync(FriendRequest request)
        {
            await _dBContext.FriendRequests.AddAsync(request);
            await _dBContext.SaveChangesAsync();

            var send = new SendFriendRequestDTO
            {
                SenderId = request.SenderId,
                ReciverId=request.ReciverId
            };

            return  send;

        }
    }
}
