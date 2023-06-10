using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }
        public UserAccount User { get; set; }



    }
}
