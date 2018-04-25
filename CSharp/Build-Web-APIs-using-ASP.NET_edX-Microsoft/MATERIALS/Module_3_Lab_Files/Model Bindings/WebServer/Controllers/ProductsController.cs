using Microsoft.AspNetCore.Mvc;
using WebServer.Models;

namespace WebServer.Controllers {

    [Route("api/[controller]")]
    public class ProductsController : Controller {
        [HttpGet]
        public Product Action1([FromQuery]Product product) {
            return product;
        }

        [HttpGet("{id}/{name}/{price}")]
        public Product Action2([FromRoute]Product product) {
            return product;
        }

        [HttpGet("{id}/{name}")]
        public Product Action3(Product product) {
            return product;
        }

        // [HttpPost]
        // public Product Action4([FromForm]Product product) {
        //     return product;
        // }

        // [HttpPost]
        // public Product Action5([FromBody]Product product) {
        //     return product;
        // }

        // [HttpPost("{id}/{name}/{price}")]
        // public Product[] Action6([FromRoute]Product p1, [FromQuery] Product p2, [FromBody]Product p3) {
        //     return new Product[] { p1, p2, p3 };
        // }

        // [HttpPost("{id}/{name}/{price}")]
        // public Product[] Action7([FromRoute]Product p1, [FromQuery] Product p2, [FromForm]Product p3) {
        //     return new Product[] { p1, p2, p3 };
        // }
    }
}