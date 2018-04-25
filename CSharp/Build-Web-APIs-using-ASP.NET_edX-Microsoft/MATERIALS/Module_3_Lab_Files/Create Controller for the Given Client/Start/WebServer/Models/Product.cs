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
            Products.Add(6, new Product { ID = 6, Name = "Guitar", Price = 11.11 });
            Products.Add(7, new Product { ID = 7, Name = "Hook", Price = 12.12 });
            Products.Add(8, new Product { ID = 8, Name = "Ice Cream", Price = 14.14 });
            Products.Add(9, new Product { ID = 9, Name = "Jawbreaker", Price = 15.15 });
            Products.Add(10, new Product { ID = 10, Name = "Knife", Price = 16.16 });
            Products.Add(11, new Product { ID = 11, Name = "Lighter", Price = 17.17 });
            Products.Add(12, new Product { ID = 12, Name = "Mug", Price = 18.18 });
        }
    }
}