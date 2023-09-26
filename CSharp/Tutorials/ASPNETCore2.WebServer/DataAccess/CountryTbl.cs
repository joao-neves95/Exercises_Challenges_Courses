using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using WebServer.Lib.Data;

namespace WebServer.DataAccess
{
    public class CountryTbl
    {
        public static async Task<IList<Dictionary<string, object>>> GetAllCountries(MySqlConnection connection)
        {
            return await MySqlObjects.QueryAsync(connection, 
                @"SELECT code, name
                  FROM Country"
            );
        }
    }
}
