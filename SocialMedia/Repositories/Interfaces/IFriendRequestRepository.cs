using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;
using SocialMedia.Models.DTO;

namespace SocialMedia.Repositories.Interfaces
{
    public interface IFriendRequestRepository
    {

        Task<List<FriendRequestDTO>> GetFriendRequestsAsync(int userId);
        Task<FriendRequest> GetSpecificFriendRequestAsync(int senderId,int reciverId);
        Task<SendFriendRequestDTO> SendFriendRequestAsync(FriendRequest request);
        Task<Friendship> AcceptFriendRequestAsync(FriendRequest request);
        Task RejectFriendRequestAsync(FriendRequest request);
    }
}
