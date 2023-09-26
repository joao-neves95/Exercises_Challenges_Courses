using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public DateTime OrderPlaced { get; set; }

#nullable enable

        public DateTime? OrderFulfilled { get; set; }

#nullable disable

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public ICollection<ProductOrder> ProductOrders { get; set; }
    }
}
