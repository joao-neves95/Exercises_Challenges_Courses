using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebServer.Controllers
{
    [Route("/")]
    [Route("/index.html")]
    public class IndexController : Controller
    {
        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult(
                new res { 
                    Title = "msg", 
                    Msg = "Hello, this is the HomePage."
                }
            );
        }

    }
    public class res
    {
        public string Title { get; set; }
        public string Msg { get; set; }
    }
}
