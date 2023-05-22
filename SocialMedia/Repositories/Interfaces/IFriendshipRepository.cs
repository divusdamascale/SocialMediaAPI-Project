using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;
using SocialMedia.Models.DTO;

namespace SocialMedia.Repositories.Interfaces
{
    public interface IFriendshipRepository
    {

        public Task<List<FriendDto>> GetFriendsAsync(int userId);
        public Task DeleteFriend(int user1,int user2);
    }
}
