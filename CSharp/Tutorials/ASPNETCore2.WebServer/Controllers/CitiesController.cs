using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using WebServer.DataAccess;
using WebServer.Models;
using WebServer.Services;
using WebServer.Lib.Data;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Authorization;

namespace WebServer.Controllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        private readonly MySqlConnection db;

        public CitiesController()
        {
            // Create a new MySql connection on controller intantiation.
            this.db = MySqlObjects.Create();
        }

        [Authorize]
        [HttpGet]
        [Produces("application/json")]
        public IActionResult Get()
        {
            // For future reference.
            //
            // List<City> cities = CityTbl.GetAllCities(db);
            // List<object> response = new List<object>();

            // foreach (City city in cities)
            // {
            //     response.Add(new { 
            //         name = city.Id,
            //         countryCode = city.CountryCode
            //      });
            // }
            // Response.ContentType = JSON;
            return Ok(CityTbl.GetAllCities(db));
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<object> Get([FromRoute]int id)
        {

            IList<Dictionary<string, object>> response = await CityTbl.GetCityById(db, id);

            if (response.Count <= 0)
                return NotFound(Json(new ErrorNotFound()));
            else
                return Ok(Json(response).Value);
        }

        [Authorize]
        [HttpGet("cc/{cc}")]
        public async Task<IActionResult> Get([FromRoute]string cc)
        {
            IList<Dictionary<string, object>> response = await CityTbl.GetCityByCountryCode(db, cc);

            if (response.Count <= 0)
                return NotFound(Json(new ErrorNotFound()));
            else
                return new OkObjectResult(response);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Post([FromBody]City city)
        {
            if (!ModelState.IsValid)
                return BadRequest(Json(Utils.GetErrorsFromModelState(ModelState)));
            
            CityTbl.AddCity(db, city);
            return Created($"api/cities/{city.Id}", city);
        }

        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromRoute]int id, [FromBody]City city)
        {
            if (!ModelState.IsValid)
                return BadRequest(Json(Utils.GetErrorsFromModelState(ModelState)));

            IList<Dictionary<string, object>> target = await CityTbl.GetCityById(db, id);
            if (target.Count <= 0)
                return NotFound(Json(new ErrorNotFound()));

            // TODO: Complete async.
            CityTbl.UpdateCity(db, id, city);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            IList<Dictionary<string, object>> target = await CityTbl.GetCityById(db, id);
            if (target.Count <= 0)
                return NotFound(Json(new ErrorNotFound()));

            // TODO: Complete async.
            CityTbl.DeleteCity(db, id);
            return Ok();
        }
    }
}

// TODO:
//
// Get all cities
// Get specific city by city ID
// Get cities by country code
// Create a new city
// Update an existing city
// Delete an existing city
