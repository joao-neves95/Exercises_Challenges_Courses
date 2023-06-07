using eShop.Ordering.Domain.Entities;

using Microsoft.Extensions.Logging;

namespace eShop.Ordering.Infrastructure.Persistence
{
    internal class OrderContextSeeder
    {
        public static async Task SeedAsync(OrderContext context, ILogger<OrderContextSeeder> logger)
        {
            if (context.Orders.Any())
            {
                return;
            }

            await context.Orders.AddRangeAsync(GetAllOrders());
            await context.SaveChangesAsync();

            logger.LogInformation("Seeded database associated with context {DbContextName}", typeof(OrderContext).Name);
        }

        private static IEnumerable<DataOrder> GetAllOrders()
        {
            return new[]
            {
                new DataOrder() {
                    UserName = "swn",
                    BillingAddress = new DataBillingAddress()
                    {
                        FirstName = "Mehmet",
                        LastName = "Ozkaya",
                        EmailAddress = "ezozkme@gmail.com",
                        AddressLine = "Bahcelievler",
                        Country = "Turkey",
                    },
                    TotalPrice = 350,
                }
            };
        }
    }
}
