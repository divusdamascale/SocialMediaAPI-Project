using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;

namespace SocialMedia.Repositories
{
    public class PostLikeRepository : IPostLikeRepository
    {
        private readonly SocialDBContext db;

        public PostLikeRepository(SocialDBContext db)
        {
            this.db = db;
        }
        public async Task<PostLike> PostLikeAsync(int UserId,int PostId)
        {

            if(await db.Posts.SingleOrDefaultAsync(p => p.PostId == PostId) == null)
            {
                return null;
            }else if(await db.PostLikes.SingleOrDefaultAsync(p =>p.PostId == PostId && p.UserId == UserId) != null)
            {
                throw new Exception("Ai apreciat deja aceasta postare");
            }

            var postLike = new PostLike
            {
                UserId = UserId,
                PostId = PostId,
                Time = DateTime.Now,
            };

            await db.PostLikes.AddAsync(postLike);
            await db.SaveChangesAsync();

            return postLike;
        }

        public async Task<PostLike> Unlike(int UserId,int PostId)
        {

            var like = await db.PostLikes.SingleOrDefaultAsync(p => p.UserId == UserId && p.PostId == PostId);
            if(like == null)
            {
                return null;
            }

            db.PostLikes.Remove(like);
            await db.SaveChangesAsync();

            return like;
        }
    }
}
