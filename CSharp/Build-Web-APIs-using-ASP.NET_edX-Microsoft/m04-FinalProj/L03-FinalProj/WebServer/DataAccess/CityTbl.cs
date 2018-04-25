using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebServer.Models;

namespace WebServer.DataAccess
{
    public class CityTbl
    {
        public static List<City> GetAllCities(WorldDbContext dbContext)
        {
            return dbContext.City.ToList();
        }

        public static List<City> GetCityById(WorldDbContext dbContext, int id)
        {
            // Parameterized input values are automaticaly converted to a "DbParameter" by EFCore, 
            //   in order to protect to protect against SQL injection attacks.
            var Id = id;
            return dbContext.City
                .FromSql(
                    $@"SELECT *
                       FROM City
                       WHERE City.ID = {Id}"
                )
                .ToList();
        }

        public static List<City> GetCityByCountryCode(WorldDbContext dbContext, string countryCode)
        {
            var CountryCode = countryCode;
            return dbContext.City
                .FromSql(
                    $@"SELECT *
                       FROM City
                       WHERE City.CountryCode = {CountryCode}"
                )
                .ToList();
        }

        public static void AddCity(WorldDbContext dbContext, City city)
        {
            var name = city.Name;
            var countryCode = city.CountryCode;
            var district = city.District;
            var population = city.Population;

            dbContext.Database
                .ExecuteSqlCommand(
                    $@"INSERT INTO City (Name, CountryCode, District, Population)
                       VALUES ({name}, {countryCode}, {district}, {population})"
                );
        }

        public static void UpdateCity(WorldDbContext dbContext, int id, City city)
        {
            var Id = id;
            var name = city.Name;
            var countryCode = city.CountryCode;
            var district = city.District;
            var population = city.Population;

            dbContext.Database
                .ExecuteSqlCommand(
                    $@"UPDATE City
                       SET Name = {name},
                           CountryCode = {countryCode},
                           District = {district},
                           Population = {population}
                       WHERE City.Id = {Id}"
                );
        }

        public static void DeleteCity(WorldDbContext dbContext, int id)
        {
            var Id = id;
            dbContext.Database
              .ExecuteSqlCommand(
                  $@"DELETE FROM City
                     WHERE City.Id = {Id}"
              );
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

