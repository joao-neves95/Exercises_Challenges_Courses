using System.Collections.Generic;

namespace WebServer.Models {
    public class Product {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }

    public class FakeData {
        public static IDictionary<int, Product> Products;
        static FakeData() {
            Products = new Dictionary<int, Product>();
            Products.Add(0, new Product { ID = 0, Name = "Apple", Price = 5.55 });
            Products.Add(1, new Product { ID = 1, Name = "Bike", Price = 6.66 });
            Products.Add(2, new Product { ID = 2, Name = "Coffee", Price = 7.77 });
        }
    }
}