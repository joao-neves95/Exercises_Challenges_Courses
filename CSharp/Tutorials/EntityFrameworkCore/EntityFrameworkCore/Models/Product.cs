using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public void Log()
        {
            Console.WriteLine($"Id: {this.Id}");
            Console.WriteLine($"Name: {this.Name}");
            Console.WriteLine($"Price: {this.Price}");
        }
    }
}
