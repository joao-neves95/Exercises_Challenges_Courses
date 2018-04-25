using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebServer.Models;

namespace WebServer.Controllers {
    [Route("api/[controller]")]
    public class RocketsController : Controller {
        [HttpGet]
        public ActionResult Get() {
            return Ok(FakeData.Rockets.Values.ToArray());
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id) {
            if (FakeData.Rockets.ContainsKey(id)) {
                return Ok(FakeData.Rockets[id]);
            } else {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody]Rocket rocket) {
            if (!this.ModelState.IsValid || rocket == null) {
                return BadRequest();
            } else {
                var maxExistingID = 0;
                if (FakeData.Rockets.Count > 0) {
                    maxExistingID = FakeData.Rockets.Keys.Max();
                }

                rocket.ID = maxExistingID + 1;
                FakeData.Rockets.Add(rocket.ID, rocket);

                return Created($"api/rockets/{rocket.ID}", rocket);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]Rocket rocket) {
            if (!this.ModelState.IsValid) {
                return BadRequest();
            } else if (FakeData.Rockets.ContainsKey(id)) {
                FakeData.Rockets[id] = rocket;
                return Ok();
            } else {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id) {
            if (FakeData.Rockets.ContainsKey(id)) {
                FakeData.Rockets.Remove(id);
                return Ok();
            } else {
                return NotFound();
            }
        }
    }
}