using System.ComponentModel.DataAnnotations;

namespace L01.Models
{
    public class Actor {
        [Key]
        public int Actor_Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string First_Name { get; set; }
        // The ErrorMessage setter is for future reference.
        [Required(ErrorMessage = "The Lirst Name field is required.")]
        [Display(Name = "Lirst Name")]
        public string Last_Name { get; set; }
    }
}
