using System.ComponentModel.DataAnnotations;

namespace WorldWebServer.Models {
    public class Country {
        [Key]
        public string Code { get; set; }
        public string Name { get; set; }
    }
}