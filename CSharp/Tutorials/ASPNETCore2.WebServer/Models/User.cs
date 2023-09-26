using System.ComponentModel.DataAnnotations;

namespace WebServer.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string IdentityId { get; set; }

        [Required(ErrorMessage = "The 'username' field is required.")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "The 'Password' field is required.")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
