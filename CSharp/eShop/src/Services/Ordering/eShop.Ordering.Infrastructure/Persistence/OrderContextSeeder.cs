using eShop.Ordering.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eShop.Ordering.Infrastructure.Persistence
{
    public sealed class OrderContextSeeder
    {
        public static async Task SeedAsync(OrderContext context, ILogger<OrderContextSeeder> logger)
        {
            if (await context.Orders.AnyAsync())
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
                    BillingInformation = new DataBillingInformation()
                    {
                        FirstName = "Mehmet",
                        LastName = "Ozkaya",
                        EmailAddress = "ezozkme@gmail.com",
                        AddressLine = "Bahcelievler",
                        Country = "Turkey",
                        State = "xyz",
                        ZipCode = "1234",
                    },
                    PaymentInformation = new DataPaymentInformation()
                    {
                        CardName = "MEHMET OZKAYA",
                        CardNumber = "1",
                        CVV = "123",
                        Expiration = "1",
                        PaymentMethod = 1,
                    },
                    TotalPrice = 350,
                }
            };
        }
    }
}
