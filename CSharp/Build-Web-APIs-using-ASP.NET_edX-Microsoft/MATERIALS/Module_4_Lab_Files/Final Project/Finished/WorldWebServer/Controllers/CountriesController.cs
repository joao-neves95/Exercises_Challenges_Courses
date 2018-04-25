using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WorldWebServer.Models;

namespace WorldWebServer.Controllers {

    [Route("api/[controller]")]
    public class CountriesController : Controller {

        private WorldDbContext dbContext;

        public CountriesController() {
            var connString = "server=localhost;port=3306;database=world;userid=root;pwd=123456;sslmode=none";
            this.dbContext = WorldDbContextFactory.Create(connString);
        }

        [HttpGet]
        public ActionResult Get() {
            return Ok(this.dbContext.Country.ToArray());
        }
    }
}