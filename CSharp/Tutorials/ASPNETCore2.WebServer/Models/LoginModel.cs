using System;
using System.ComponentModel.DataAnnotations;

namespace WebServer.Models
{
    public class LoginDto
    {
        [Required(ErrorMessage = "The Email field is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Password field is required.")]
        public string Password { get; set; }
    }
}
