using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using WebServer.Lib.Data;
using WebServer.DataAccess;

namespace WebServer.Controllers
{
    [Route("api/countries")]
    public class CountriesController : Controller
    {
        private MySqlConnection db;

        public CountriesController()
        {
            this.db = MySqlObjects.Create();
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Json(CountryTbl.GetAllCountries(db));
        }
    }
}
