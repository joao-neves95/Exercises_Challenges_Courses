using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

#nullable enable

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

#nullable disable

        public ICollection<Order> Orders { get; set; }
    }
}
