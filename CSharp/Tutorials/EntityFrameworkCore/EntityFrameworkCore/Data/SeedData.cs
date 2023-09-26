using EntityFrameworkCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Data
{
    public static class SeedData
    {
        public static void Init()
        {
            using TheDbContext dbContext = new TheDbContext();

            Product prod1 = new Product()
            {
                Name = "1ihgfcyitfty",
                Price = 9999.98m
            };

            dbContext.Products.Add(prod1);

            Product prod2 = new Product()
            {
                Name = "2jhbsd",
                Price = 887.45m
            };

            dbContext.Products.Add(prod2);

            dbContext.SaveChanges();
        }

        public static void ShowAll()
        {
            using TheDbContext dbContext = new TheDbContext();

            IOrderedQueryable<Product> products = dbContext.Products
                                                           .OrderBy(prod => prod.Price)
                                                           ;

            foreach(Product product in products)
            {
                product.Log();
                Console.WriteLine("");
            }

            Console.WriteLine("Update the second one by id...");
            Console.WriteLine("30% discount!");

            Product? scndProd = dbContext.Products
                                         .Where(prod => prod.Id == 2)
                                         .FirstOrDefault()
                                         ;

            if (scndProd != default)
            {
                scndProd.Price *= 0.80m;
                //scndProd.Price = 887.45m;
                dbContext.SaveChanges();
                scndProd.Log();
            }
            else
            {
                Console.WriteLine("Product not found!");
            }
        }
    }
}
