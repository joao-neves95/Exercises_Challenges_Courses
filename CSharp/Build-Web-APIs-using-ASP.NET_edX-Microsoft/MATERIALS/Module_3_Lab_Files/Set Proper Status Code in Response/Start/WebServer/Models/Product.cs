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
            Products.Add(3, new Product { ID = 3, Name = "Duck", Price = 8.88 });
            Products.Add(4, new Product { ID = 4, Name = "Earphone", Price = 9.99 });
            Products.Add(5, new Product { ID = 5, Name = "Freezer", Price = 10.10 });
        }
    }
}