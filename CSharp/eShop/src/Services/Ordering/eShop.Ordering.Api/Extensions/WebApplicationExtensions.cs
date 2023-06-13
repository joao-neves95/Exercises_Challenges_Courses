using eShop.Ordering.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Threading;

namespace eShop.Ordering.Api.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> MigrateDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<OrderContextSeeder>>();
            var dbContext = scope.ServiceProvider.GetService<OrderContext>();

            await using var transaction = await dbContext.Database.BeginTransactionAsync(IsolationLevel.Serializable);

            try
            {
                logger.LogInformation("Migrating database associated with context {DbContextName}.", nameof(OrderContext));

                await dbContext.Database.MigrateAsync();

                if (app.Environment.IsDevelopment())
                {
                    await OrderContextSeeder.SeedAsync(dbContext, logger);
                }

                await transaction.CommitAsync();

                logger.LogInformation("Migrated database associated with context {DbContextName}.", nameof(OrderContext));
            }
            catch (SqlException ex)
            {
                await transaction.RollbackAsync();

                logger.LogError(
                    "Error while migrating database associated with context {DbContextName}. e={@ex}",
                    nameof(OrderContext),
                    ex);

                throw;
            }

            return app;
        }
    }
}
