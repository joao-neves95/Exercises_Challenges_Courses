using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebServer.DataAccess;
using WebServer.Models;
using WebServer.Lib;

namespace WebServer.Controllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        private WorldDbContext db;

        public CitiesController()
        {
            this.db = WorldDbContextFactory.Create();
        }

        [HttpGet]
        public ActionResult Get()
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
            return Json(CityTbl.GetAllCities(db));
        }

        [HttpGet("{id:int}")]
        public ActionResult Get([FromRoute]int id)
        {
            List<City> response = CityTbl.GetCityById(db, id);

            if (response.Count == 0)
                return NotFound();
            else
                return Json(response);
        }

        [HttpGet("cc/{cc}")]
        public ActionResult Get([FromRoute]string cc)
        {
            List<City> response = CityTbl.GetCityByCountryCode(db, cc);

            if (response.Count == 0)
                return NotFound();
            else
                return Json(response);
        }

        [HttpPost]
        public ActionResult Post([FromBody]City city)
        {
            if (!ModelState.IsValid)
                return BadRequest(Json(Utils.GetErrorsFromModelState(ModelState)));
            
            CityTbl.AddCity(db, city);
            return Created($"api/cities/{city.Id}", city);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put([FromRoute]int id, [FromBody]City city)
        {
            if (!ModelState.IsValid)
                return BadRequest(Json(Utils.GetErrorsFromModelState(ModelState)));

            List<City> target = CityTbl.GetCityById(db, id);
            if (target.Count == 0)
                return NotFound();

            CityTbl.UpdateCity(db, id, city);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete([FromRoute]int id)
        {
            List<City> target = CityTbl.GetCityById(db, id);
            if (target.Count == 0)
                return NotFound();

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
