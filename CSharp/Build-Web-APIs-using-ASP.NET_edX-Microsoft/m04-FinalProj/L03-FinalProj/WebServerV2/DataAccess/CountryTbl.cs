using System.Collections.Generic;
using MySql.Data.MySqlClient;
using WebServer.Lib.Data;

namespace WebServer.DataAccess
{
    public class CountryTbl
    {
        public static List<Dictionary<string, object>> GetAllCountries(MySqlConnection connection)
        {
            return MySqlObjects.Query(connection, 
                @"SELECT *
                  FROM Country"
            );
        }
    }
}
