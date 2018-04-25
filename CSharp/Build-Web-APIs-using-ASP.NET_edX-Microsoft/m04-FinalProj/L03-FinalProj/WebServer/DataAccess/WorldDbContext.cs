using Microsoft.EntityFrameworkCore;
using WebServer.Models;

namespace WebServer.DataAccess
{
    public class WorldDbContext : DbContext
    {
        public DbSet<City> City { get; set; }
        public DbSet<Country> Country { get; set; }

        public WorldDbContext(DbContextOptions<WorldDbContext> options) : base(options) { }
    }
}
