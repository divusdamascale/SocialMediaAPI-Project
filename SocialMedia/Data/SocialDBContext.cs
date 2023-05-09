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
        }


        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }
    }
}
