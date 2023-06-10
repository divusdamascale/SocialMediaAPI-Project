namespace SocialMedia.Models.DTO
{
    public class FriendPostDTO
    {
        public int PostId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        //public UserAccount User { get; set; }
    }
}
