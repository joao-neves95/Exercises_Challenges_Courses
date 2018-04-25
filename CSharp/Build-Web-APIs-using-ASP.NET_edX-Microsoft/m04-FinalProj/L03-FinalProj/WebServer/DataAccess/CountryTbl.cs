using System.Linq;
using System.Collections.Generic;
using WebServer.Models;

namespace WebServer.DataAccess
{
    public class CountryTbl
    {
        public static List<Country> GetAllCountries(WorldDbContext dbContext)
        {
            return dbContext.Country.ToList();
        }
    }
}
