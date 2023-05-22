using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using SocialMedia.Models;
using SocialMedia.Models.DTO;
using SocialMedia.Repositories.Interfaces;

namespace SocialMedia.Repositories
{
    public class FriendshipRepository : IFriendshipRepository
    {
        private readonly SocialDBContext db;

        public FriendshipRepository(SocialDBContext db)
        {
            this.db = db;
        }

        public async Task DeleteFriend(int user1,int user2)
        {
            var friend = await db.Friendships.Where(x => x.User1Id == user1 && x.User2Id == user2).FirstOrDefaultAsync();
            var friendReverse = await db.Friendships.Where(x => x.User1Id == user2 && x.User2Id == user1).FirstOrDefaultAsync();

            if(friend == null) { throw new Exception("Nu exista prietenia"); }
            if(friendReverse == null) { throw new Exception("Ceva chiar nu e inregula"); }

            db.Friendships.Remove(friend);
            db.Friendships.Remove(friendReverse);

            await db.SaveChangesAsync();
        }

        public async Task<List<FriendDto>> GetFriendsAsync(int userId)
        {
            var list = await db.Friendships
               .Include(fr => fr.User2)
               .Where(fr => fr.User1Id == userId)
               .Select(fr => new FriendDto
               {
                   FriendId = fr.User2Id,
                   FriendName = fr.User2.Name,
                   Email = fr.User2.Email,
                   Country = fr.User2.Country,
                   DOB = fr.User2.DateOfBirth,
               })
               .ToListAsync<FriendDto>();

            return list;
        }
    }
}
