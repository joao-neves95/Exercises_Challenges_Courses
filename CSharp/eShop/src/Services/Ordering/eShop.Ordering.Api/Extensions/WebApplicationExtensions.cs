using eShop.Ordering.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace eShop.Ordering.Api.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> MigrateDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<OrderContextSeeder>>();
            var dbContext = scope.ServiceProvider.GetService<OrderContext>();

            try
            {
                logger.LogInformation("Migrating database associated with context {DbContextName}", nameof(OrderContext));

                await dbContext.Database.MigrateAsync();

                if (app.Environment.IsDevelopment())
                {
                    await OrderContextSeeder.SeedAsync(dbContext, logger);
                }

                logger.LogInformation("Migrated database associated with context {DbContextName}", nameof(OrderContext));
            }
            catch (SqlException)
            {
                throw;
            }

            return app;
        }
    }
}
