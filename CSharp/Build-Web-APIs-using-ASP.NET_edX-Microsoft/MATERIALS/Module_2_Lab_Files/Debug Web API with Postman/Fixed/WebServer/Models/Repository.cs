using System.Collections.Generic;

namespace WebServer.Models {
    public class Repository {
        public static IDictionary<int, Product> Products { get; private set; }

        static Repository() {
            Products = new Dictionary<int, Product>();
            Products[0] = new Product { ID = 0, Name = "Apple", Price = 1.99 };
            Products[1] = new Product { ID = 1, Name = "Bike", Price = 99.99 };
            Products[2] = new Product { ID = 2, Name = "Donuts", Price = 2.99 };
            Products[3] = new Product { ID = 3, Name = "Coffee", Price = 5.99 };
        }
    }
}