using Microsoft.EntityFrameworkCore;
using L01.Models;

namespace L01.DataAccess
{
    public class SakilaDbContext : DbContext
    {
        public virtual DbSet<Actor> Actor { get; set; }

        public SakilaDbContext(DbContextOptions<SakilaDbContext> options) 
            : base(options) { }
    }
}
