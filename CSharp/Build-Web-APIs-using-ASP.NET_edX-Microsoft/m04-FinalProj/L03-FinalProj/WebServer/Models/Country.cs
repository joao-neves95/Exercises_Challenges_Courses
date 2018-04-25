using System.ComponentModel.DataAnnotations;

namespace WebServer.Models
{
    public class Country
    {
        [Key]
        public string Code { get; set; }
        [Required(ErrorMessage = "The 'name' field is required.")]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
