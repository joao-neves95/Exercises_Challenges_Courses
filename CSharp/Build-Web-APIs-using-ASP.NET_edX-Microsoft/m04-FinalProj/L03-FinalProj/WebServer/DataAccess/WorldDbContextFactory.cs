using Microsoft.EntityFrameworkCore;

namespace WebServer.DataAccess
{
    public class WorldDbContextFactory
    {
        private static readonly string SERVER = DotNetEnv.Env.GetString("SERVER");
        private static readonly string PORT = DotNetEnv.Env.GetString("PORT");
        private static readonly string USER_ID = DotNetEnv.Env.GetString("USER_ID");
        private static readonly string connectionString = $"server={SERVER};port={PORT};database=world;userid={USER_ID};sslmode=none";

        public static WorldDbContext Create()
        {
            DbContextOptionsBuilder<WorldDbContext> optionsBuilder = new DbContextOptionsBuilder<WorldDbContext>();
            optionsBuilder.UseMySQL(connectionString);
            WorldDbContext dbContext = new WorldDbContext(optionsBuilder.Options);
            return dbContext;
        }
    }
}
