using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using WebServer.Lib.Data;
using WebServer.Models;

namespace WebServer.DataAccess
{
    public class CityTbl
    {
        public static async Task<IList<Dictionary<string, object>>> GetAllCities(MySqlConnection connection)
        {
            return await MySqlObjects.QueryAsync(connection, 
                @"SELECT *
                  FROM City"
            );
        }

        public static async Task<IList<Dictionary<string, object>>> GetCityById(MySqlConnection connection, int id)
        {
            MySqlCommand query = new MySqlCommand(
                @"SELECT *
                  FROM City
                  WHERE City.ID = @ID", 
                connection
            );
            query.Parameters.AddWithValue("@ID", id);
            return await MySqlObjects.QueryAsync(connection, query);    
        }

        public static async Task<IList<Dictionary<string, object>>> GetCityByCountryCode(MySqlConnection connection, string countryCode)
        {
            MySqlCommand query = new MySqlCommand(
                @"SELECT *
                  FROM City
                  WHERE City.CountryCode = @CountryCode",
                connection
            );
            query.Parameters.AddWithValue("@CountryCode", countryCode);
            return await MySqlObjects.QueryAsync(connection, query);
        }

        public static void AddCity(MySqlConnection connection, City city)
        {
            MySqlObjects.Command(connection,
                @"INSERT INTO City (Name, CountryCode, District, Population)
                  VALUES (@name, @countryCode, @district, @population)",
                  new Dictionary<string, object> 
                  {
                      { "@name", city.Name },
                      { "@countryCode", city.CountryCode },
                      { "@district", city.District },
                      { "@population", city.Population }
                  }
            );
            // This was to test performance. It's the same.
            // 
            // MySqlCommand cmd = new MySqlCommand(
            //     $@"INSERT INTO City (Name, CountryCode, District, Population)
            //        VALUES (@name, @countryCode, @district, @population)",
            //     connection
            // );
            // cmd.Parameters.AddWithValue("@name", city.Name);
            // cmd.Parameters.AddWithValue("@countryCode", city.CountryCode);
            // cmd.Parameters.AddWithValue("@district", city.District);
            // cmd.Parameters.AddWithValue("@population", city.Population);
            // MySqlObjects.Command(connection, cmd);
        }

        public static void UpdateCity(MySqlConnection connection, int id, City city)
        {
            MySqlObjects.Command(connection,
              @"UPDATE City
                SET Name = @name,
                    CountryCode = @countryCode,
                    District = @district,
                    Population = @population
                WHERE City.Id = @id",
                new Dictionary<string, object> 
                {
                    { "@name", city.Name },
                    { "@countryCode", city.CountryCode },
                    { "@district", city.District },
                    { "@population", city.Population },
                    { "@id", id }
                }
            );
        }

        public static void DeleteCity(MySqlConnection connection, int id)
        {
            MySqlCommand cmd = new MySqlCommand(
                @"DELETE FROM City
                  WHERE City.Id = @Id",
                connection
            );
            cmd.Parameters.AddWithValue("@Id", id);
            MySqlObjects.Command(connection, cmd);
        }
    }
}
