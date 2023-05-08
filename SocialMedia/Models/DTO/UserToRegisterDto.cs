using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models.DTO
{
    public class UserToRegisterDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8,ErrorMessage ="Min 8 characters")]
        

        public string Password { get; set; }
        [Required]

        public string Country { get; set; }
        [Required]
        [DataType(DataType.DateTime)]

        public DateTime DateOfBirth { get; set; }
        [Required]
        [DataType(DataType.Text)]

        public string Name { get; set; }
    }
}
