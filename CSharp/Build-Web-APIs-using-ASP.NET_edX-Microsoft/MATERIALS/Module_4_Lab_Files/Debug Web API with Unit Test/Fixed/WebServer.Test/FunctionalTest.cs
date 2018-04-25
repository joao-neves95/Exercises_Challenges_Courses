using System;
using Xunit;
using WebServer.Models;
using WebServer.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace WebServer.Test {
    public class FunctionalTest {

        [Fact]
        public void CreateProductsControllerInstanceTest() {
            var controller = new ProductsController();
            Assert.NotNull(controller);
        }

        [Fact]
        public void RepositoryInitializtionTest() {
            Assert.NotNull(Repository.Products);
            Assert.Equal(Repository.Products.Count, 4);

            foreach (var id in new int[] { 0, 1, 2, 3 }) {
                Assert.True(Repository.Products.ContainsKey(id));
            }

            foreach (var key in Repository.Products.Keys) {
                Assert.Equal(Repository.Products[key].ID, key);
            }
        }

        [Fact]
        public void GetActionTest() {
            var controller = new ProductsController();
            Assert.IsType<OkObjectResult>(controller.Get());
            foreach (var key in Repository.Products.Keys) {
                Assert.IsType<OkObjectResult>(controller.Get(key));
            }
        }

        [Fact]
        public void PostActionTest() {
            var controller = new ProductsController();
            int oldCount = Repository.Products.Count;
            var product = new Product { Name = "Test Product", Price = 9.9 };
            Assert.IsType<CreatedResult>(controller.Post(product));
            Assert.Equal(Repository.Products.Count, oldCount + 1);
        }

        [Fact]
        public void DeleteActionTest() {
            var controller = new ProductsController();
            int oldCount = Repository.Products.Count;
            var maxKey = Repository.Products.Keys.Max();
            Assert.IsType<OkResult>(controller.Delete(maxKey));
            Assert.Equal(Repository.Products.Count, oldCount - 1);
        }

        [Fact]
        public void PutActionTest() {
            var controller = new ProductsController();
            int oldCount = Repository.Products.Count;
            var maxKey = Repository.Products.Keys.Max();
            var product = Repository.Products[maxKey];
            product.Name = "Changed";
            Assert.IsType<OkResult>(controller.Put(maxKey, product));
            Assert.Equal(Repository.Products.Count, oldCount);
        }
    }
}
