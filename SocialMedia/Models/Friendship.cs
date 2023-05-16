namespace SocialMedia.Models
{
    public class Friendship
    {
        public int User1Id { get; set; }
        public int User2Id { get; set; }
        public DateTime Date { get; set; }
        public UserAccount User1 { get; set; }
        public UserAccount User2 { get; set; }

    }
}
