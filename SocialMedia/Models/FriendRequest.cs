namespace SocialMedia.Models
{
    public class FriendRequest
    {
        public int SenderId { get; set; }
        public int ReciverId { get; set; }

        public DateTime Date { get; set; }

        public UserAccount Sender { get; set; }
        public UserAccount Reciver { get; set; }

    }
}
