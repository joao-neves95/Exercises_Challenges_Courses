using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using WebServer.Lib.Data;
using WebServer.DataAccess;

namespace WebServer.Controllers
{
    [Route("api/countries")]
    public class CountriesController : Controller
    {
        private readonly MySqlConnection _db;

        public CountriesController()
        {
            _db = MySqlObjects.Create();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(CountryTbl.GetAllCountries(_db));
        }
    }
}
