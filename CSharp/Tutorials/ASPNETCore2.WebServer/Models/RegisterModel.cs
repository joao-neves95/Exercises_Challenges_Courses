using System.ComponentModel.DataAnnotations;

namespace WebServer.Models
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "Wrong email format.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Password field is required.")]
        [DataType(DataType.Password)]
        [StringLength(255, ErrorMessage = "The Password must have at least 8 characters.", MinimumLength = 8)]
        public string Password { get; set; }
    }
}
