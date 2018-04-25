using System;
using Microsoft.AspNetCore.Mvc;
using SelfAssessment.Models;
using SelfAssessment.Services;

namespace SelfAssessment.Controllers
{
    [Route("api/products")]
    public class ProductsController : Controller
    {
        [HttpGet]
        public ActionResult Get()
        {
            return ProductsServices.GetAllProducts();
        }

        [HttpGet("{id}")]
        public ActionResult Get([FromRoute]int id)
        {
            JsonResult response = ProductsServices.GetProductById(id);

            if (response.Value.Equals(Status.NotFound))
                return NotFound();
            else
                return response;
            
        }

        [HttpGet("price/{from}/{to}")]
        public ActionResult Get([FromRoute]int from, [FromRoute]int to)
        {
            JsonResult response = ProductsServices.GetProductPriceBetween(from, to);

            if (response.Value.Equals(Status.NotFound))
                return NotFound();
            else
                return response;
        }

        [HttpPost]
        public ActionResult Post([FromBody]Product product)
        {
            ProductsServices.PostProduct(product);
            return StatusCode(201, product);
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromRoute]int id, [FromBody]Product product)
        {
            object response = ProductsServices.UpdateProduct(id, product);

            if (response.Equals(Status.Ok))
              return Ok();
            else
              return NotFound();
        }

        [HttpPut("raise/{ammount}")]
        public ActionResult Raise([FromRoute]int ammount)
        {
            ProductsServices.RaisePriceAllProducts(ammount);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute]int id)
        {
            object response = ProductsServices.DeleteProduct(id);

            if (response.Equals(Status.Ok))
              return Ok();
            else
              return NotFound();
        }
    }
}
