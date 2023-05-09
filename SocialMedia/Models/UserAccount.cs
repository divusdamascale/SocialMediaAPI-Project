using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models
{
    public class UserAccount
    {
        [Key]
        public int UserId { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get;set; } 
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Name { get; set; }

        public ICollection<FriendRequest> FriendRequestsSend { get; set; }
        public ICollection<FriendRequest> FriendRequestsRecived { get; set; }


    }
}
