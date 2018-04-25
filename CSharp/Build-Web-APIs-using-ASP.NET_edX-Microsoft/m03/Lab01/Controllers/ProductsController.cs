using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Lab01.Models;

namespace Lab01.Controllers
{
    [Route("api/products")]
    public class ProductsController : Controller
    {
        [HttpGet("all")]
        public ActionResult Get() 
        {
            return Json(Data.Products.Values.ToArray());
        }

        [HttpGet]
        public ActionResult Get([FromQuery]int id) 
        {
            if (Data.Products.ContainsKey(id))
              return Json(Data.Products[id]);
            else
              return NotFound();
        }

        [HttpPost]
        public ActionResult Post([FromBody]Product product)
        {
            product.ID = Data.Products.Count();
            Data.Products.Add(product.ID, product);
            return Created($"api/products?id={product.ID}", Json(product));
        }

        [HttpPut()]
        public ActionResult Put([FromQuery]int id, [FromBody]Product product)
        {
            if (Data.Products.ContainsKey(id))
            {
                Product target = Data.Products[id];
                target.ID = product.ID;
                target.Name = product.Name;
                target.Price = product.Price;
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete()]
        public ActionResult Delete([FromQuery]int id)
        {
            if (Data.Products.ContainsKey(id))
            {
                Data.Products.Remove(id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
