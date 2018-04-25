using Microsoft.AspNetCore.Mvc;
using WebServer.Models;
using System.Linq;

namespace WebServer.Controllers
{
    [Route("api/people")]
    public class PeopleController : Controller
    {
        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult(Repository.People.Values.ToArray());
        }

        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            return new JsonResult(Repository.GetPersonById(id));
        }

        [HttpPost]
        public void Post([FromBody]Person person)
        {
            Repository.AddPerson(person);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Person person)
        {
            Repository.ReplacePersonByID(id, person);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Repository.RemovePersonById(id);
        }
    }
}
