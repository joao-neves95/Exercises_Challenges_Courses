using eShop.Ordering.Domain.Common;
using eShop.Ordering.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace eShop.Ordering.Infrastructure.Persistence
{
    public sealed class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options)
            : base(options)
        {
        }

        public DbSet<DataOrder> Orders { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.Entity.LastUpdatedDate = DateTime.UtcNow;
                        entry.Entity.LastUpdatedBy = "test";
                        break;
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        entry.Entity.CreatedBy = "test";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
