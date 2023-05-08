using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models.DTO
{
    public class UserToLoginDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } 

    }
}
