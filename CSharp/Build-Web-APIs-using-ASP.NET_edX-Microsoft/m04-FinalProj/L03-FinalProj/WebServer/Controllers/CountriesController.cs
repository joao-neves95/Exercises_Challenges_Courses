using Microsoft.AspNetCore.Mvc;
using WebServer.DataAccess;

namespace WebServer.Controllers
{
    [Route("api/countries")]
    public class CountriesController : Controller
    {
        private WorldDbContext db;

        public CountriesController()
        {
            this.db = WorldDbContextFactory.Create();
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Json(CountryTbl.GetAllCountries(db));
        }
    }
}
