using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models
{
    public class PostComment
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }

        public string Comment { get; set; }
        public DateTime Time { get; set; }

        public Post Post { get; set; }
        public UserAccount Giver { get; set; }
    }
}
