using System.ComponentModel.DataAnnotations;

namespace WebServer.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "The 'name' field is required.")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The 'countryCode' field is required.")]
        [Display(Name = "Country Code")]
        public string CountryCode { get; set; }
        [Required(ErrorMessage = "The 'district' field is required.")]
        [Display(Name = "District")]
        public string District { get; set; }
        [Required(ErrorMessage = "The 'population' field is required.")]
        [Display(Name = "Population")]
        public int Population { get; set; }
    }
}
