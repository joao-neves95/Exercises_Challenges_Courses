using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

// This is to return index.html with "http://localhost:5000/".
namespace WebServer.Controllers
{
    [Route("/")]
    public class IndexController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            string index = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html");
            return PhysicalFile(index, "text/html");
        }
    }
}
