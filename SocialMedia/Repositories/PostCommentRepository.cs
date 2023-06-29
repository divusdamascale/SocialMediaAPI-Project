using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using SocialMedia.Models;
using SocialMedia.Models.DTO;
using SocialMedia.Repositories.Interfaces;

namespace SocialMedia.Repositories
{
    public class PostCommentRepository:IPostCommentRepository
    {
        private readonly SocialDBContext db;

        public PostCommentRepository(SocialDBContext db)
        {
            this.db = db;
        }

        public async Task AddComment(CommentDTO comment,int postId,int userId)
        {
            if(await db.Posts.SingleOrDefaultAsync(p => p.PostId == postId) == null)
            {
                throw new Exception("Postarea nu exista");
            }

            var commentToAdd = new PostComment
            {
                UserId = userId,
                PostId = postId,
                Comment = comment.Comment,
                Time = DateTime.Now,
            };

            await db.PostComments.AddAsync(commentToAdd);
            await db.SaveChangesAsync();

        }

        public async Task DeleteComment(int commentId)
        {
            var comment = await db.PostComments.SingleOrDefaultAsync(p => p.Id == commentId);

            if(comment == null) { throw new Exception("Comentariul nu exista"); }

            db.PostComments.Remove(comment);
            await db.SaveChangesAsync();

        }
    }
}
