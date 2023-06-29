using SocialMedia.Models.DTO;

namespace SocialMedia.Repositories.Interfaces
{
    public interface IPostCommentRepository
    {

        public Task AddComment(CommentDTO comment,int postId, int userId);
        public Task DeleteComment(int commentId);

    }
}
