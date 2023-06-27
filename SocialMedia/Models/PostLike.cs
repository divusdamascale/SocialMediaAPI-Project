namespace SocialMedia.Models
{
    public class PostLike
    {
        public int UserId { get; set; }
        public int PostId { get; set; } 
        public DateTime Time { get; set; }

        public Post Post { get; set; }
        public UserAccount Giver { get; set; }
    }
}
