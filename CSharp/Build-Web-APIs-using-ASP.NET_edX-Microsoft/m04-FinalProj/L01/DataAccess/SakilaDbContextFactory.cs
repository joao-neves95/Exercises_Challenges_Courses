using Microsoft.EntityFrameworkCore;
using DotNetEnv;

namespace L01.DataAccess
{
    public class SakilaDbContextFactory
    {
        private static readonly string SERVER = DotNetEnv.Env.GetString("SERVER");
        private static readonly string PORT = DotNetEnv.Env.GetString("PORT");
        private static readonly string USER_ID = DotNetEnv.Env.GetString("USER_ID");
        private static readonly string connectionString = $"server={SERVER};port={PORT};database=sakila;userid={USER_ID};sslmode=none";
        
        public static SakilaDbContext Create()
        {
            DbContextOptionsBuilder<SakilaDbContext> optionsBuilder = new DbContextOptionsBuilder<SakilaDbContext>();
            optionsBuilder.UseMySQL(connectionString);
            SakilaDbContext sakilaDbContext = new SakilaDbContext(optionsBuilder.Options);
            return sakilaDbContext;
        }
    }
}
