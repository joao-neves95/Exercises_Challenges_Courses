using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

// This is to return index.html with "http://localhost:5000/".
namespace WebServer.Controllers
{
    [Route("/")]
    public class IndexController : Controller
    {
        // Login Page.
        [HttpGet]
        public IActionResult Get()
        {
            Response.ContentType = "text/html";
            string index = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html");
            return PhysicalFile(index, "text/html");
        }

        [HttpGet("/app")]
        [Authorize]
        public IActionResult GetPrivate()
        {
            // Accessing the User's ID for future reference.
            string userId = User.Claims.ToList()[2].Value;
            Response.ContentType = "text/html";
            string index = Path.Combine(Directory.GetCurrentDirectory(), "Pages", "index.html");
            return PhysicalFile(index, "text/html");
        }
    }
}
