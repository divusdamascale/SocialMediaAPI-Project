using SocialMedia.Models;

namespace SocialMedia.Repositories.Interfaces
{
    public interface IPostLikeRepository
    {

        public Task<PostLike> PostLikeAsync(int UserId, int PostId);

        public Task<PostLike> Unlike(int UserId,int PostId);


    }
}
