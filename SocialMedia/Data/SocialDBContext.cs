using Microsoft.EntityFrameworkCore;
using SocialMedia.Models;

namespace SocialMedia.Data
{
    public class SocialDBContext : DbContext
    {
        public SocialDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FriendRequest>(b =>
            {
                b.HasKey(x => new { x.SenderId,x.ReciverId });

                b.HasOne(x => x.Sender)
                .WithMany(x => x.FriendRequestsSend)
                .HasForeignKey(x => x.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(x => x.Reciver)
                .WithMany(x => x.FriendRequestsRecived)
                .HasForeignKey(x => x.ReciverId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Friendship>(b =>
            {
                b.HasKey(x => new { x.User1Id,x.User2Id });

                b.HasOne(x => x.User1)
                .WithMany(x => x.Friends)
                .HasForeignKey(x => x.User1Id)
                .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(x => x.User2)
                .WithMany(x => x.FriendsOf)
                .HasForeignKey(b => b.User2Id)
                .OnDelete(DeleteBehavior.Restrict);
            });


            modelBuilder.Entity<UserAccount>()
                .HasMany(p => p.Posts)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<PostLike>(b =>
            {
                b.HasKey(x => new { x.PostId,x.UserId });

                b.HasOne(x => x.Giver)
                .WithMany(x => x.PostLikes)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(x => x.Post)
                .WithMany(x => x.PostLikes)
                .HasForeignKey(x => x.PostId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PostComment>(b =>
            {

                b.HasOne(x => x.Giver)
                .WithMany(x => x.PostComments)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(x => x.Post)
                .WithMany(x => x.PostComments)
                .HasForeignKey(x => x.PostId)
                .OnDelete(DeleteBehavior.Restrict);
            });


        }


        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Post> Posts { get; set; }

        public DbSet<PostLike> PostLikes { get; set; }

        public DbSet<PostComment> PostComments { get; set; }
    }
}
