using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using SelfAssessment.Models;

namespace SelfAssessment.Services
{
    public class ProductsServices
    {
        public static JsonResult GetAllProducts()
        {
            return new JsonResult(FakeData.Products.Values.ToArray());
        }

        public static JsonResult GetProductById(int id)
        {
            if (FakeData.Products.ContainsKey(id))
                return new JsonResult(FakeData.Products[id]);
            else
                return new JsonResult(Status.NotFound);
        }

        public static JsonResult GetProductPriceBetween(int From, int To)
        {
            var products = from product in FakeData.Products.Values
                               where product.Price >= From && product.Price <= To
                               select product;

            if (products.Count() > 0)
                return new JsonResult(products.ToArray());
            else
                return new JsonResult(Status.NotFound);
        }

        public static void PostProduct(Product product)
        {
            product.ID = FakeData.Products.Count();
            FakeData.Products.Add(product.ID, product);
        }

        public static object UpdateProduct(int id, Product product)
        {
            if (FakeData.Products.ContainsKey(id)) 
            {
                FakeData.Products[id] = product;
                return Status.Ok;
            }
            else
               return Status.NotFound;
        }

        public static void RaisePriceAllProducts(int ammount)
        {
            foreach(KeyValuePair<int, Product> entry in FakeData.Products)
            {
                entry.Value.Price += ammount;
            }
        }

        public static object DeleteProduct(int id)
        {
            if (FakeData.Products.ContainsKey(id))
            {
                FakeData.Products.Remove(id);
                return Status.Ok;
            }
            else
                return Status.NotFound;
        }
    }
}