using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using SocialMedia.Models;
using SocialMedia.Models.DTO;
using SocialMedia.Repositories.Interfaces;

namespace SocialMedia.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly SocialDBContext social;

        public PostRepository(SocialDBContext social)
        {
            this.social = social;
        }



        public async Task<IEnumerable<MyPostDTO>> GetMyPosts(int myUserId)
        {
            var posts = await social.Posts
                .Where(p => p.UserId == myUserId)
                .Select(p => new MyPostDTO
                {
                    PostId = p.PostId,
                    Content = p.Content,
                    CreatedAt = p.CreatedAt,

                })
                .ToListAsync();

            return posts;
        }


        public async Task<Post> CreatePost(Post post)
        {
            await social.Posts.AddAsync(post);
            await social.SaveChangesAsync();

            return post;

        }

        public async Task<IEnumerable<FriendPostDTO>> GetFriendsPost(int myUserId)
        {

            //IdUrile Prietenilor
            var friendsIds = await social.Friendships
                .Where(f => f.User1Id == myUserId)
                .Select(f => f.User2Id)
                .ToListAsync();

            var posts = await social.Posts
               .Where(p => friendsIds.Contains(p.UserId))
               .OrderByDescending(p => p.CreatedAt)
               .Select(p => new FriendPostDTO
               {
                   PostId = p.PostId,
                   Content = p.Content,
                   CreatedAt = p.CreatedAt,
                   UserId = p.UserId,
                   Name = p.User.Name
               })
               .ToListAsync();

            return posts;

        }

        public async Task<IEnumerable<FriendPostDTO>> GetxPosts(int UserId)
        {
            var post = await social.Posts
                .Where(p => p.UserId == UserId)
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new FriendPostDTO
                {
                    PostId = p.PostId,
                    Content = p.Content,
                    CreatedAt = p.CreatedAt,
                    UserId = p.UserId,
                    Name = p.User.Name
                })
                .ToListAsync();

            return post;
        }

        public async Task<IEnumerable<ViewLikeDTO>> ViewLikes(int postId)
        {
            if(await social.Posts.SingleOrDefaultAsync(p=> p.PostId == postId) == null)
            {
                return null;
            }

            var likes = await social.PostLikes
                .Where(p => p.PostId == postId)
                .OrderByDescending(p => p.Time)
                .Select(p => new ViewLikeDTO
                {
                    UserId = p.UserId,
                    PostId = p.PostId,
                    Time = p.Time,
                })
                .ToListAsync();

            return likes;

        }
    }
}
