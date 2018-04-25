using System;
using Newtonsoft.Json;

namespace JSONDeSerialization
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create instance of the model.
            var product1 = new Product {
                ID = 101,
                Name = "Coca-Cola",
                Price = 3.89
            };

            // Serialize the instance into string.
            var jsonString = JsonConvert.SerializeObject(product1);
            Console.WriteLine(jsonString);

            // Deserialize the JSON string back to the Project class instance.
            var product2 = JsonConvert.DeserializeObject<Product>(jsonString);
            Console.WriteLine($"The product {product2.Name} ID is {product2.ID} and costs {product2.Price}.");
        }
    }

    // Model class.
    class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
