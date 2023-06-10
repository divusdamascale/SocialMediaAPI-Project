using SocialMedia.Models;
using SocialMedia.Models.DTO;

namespace SocialMedia.Repositories.Interfaces
{
    public interface IPostRepository
    {

        public Task<IEnumerable<MyPostDTO>> GetMyPosts(int myUserId);

        public Task<Post> CreatePost(Post post);

        public Task<IEnumerable<FriendPostDTO>> GetFriendsPost(int myUserId);

    }
}
