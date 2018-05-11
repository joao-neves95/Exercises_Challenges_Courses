using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebServer.Lib.Data;

namespace WebServer.Services.Data
{
    public class MySqlDbContext : IdentityDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(MySqlObjects.GetConnectionString());
        }
    }
}
