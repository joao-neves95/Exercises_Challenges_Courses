using eShop.Discount.Api.Data;
using eShop.Discount.Api.Models.Config;

using Microsoft.Extensions.Options;

namespace eShop.Discount.Api.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void SeedPgsqlDevelopmentData(this WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                return;
            }

            var pgConfig = app.Services.GetService<IOptions<PgsqlConfig>>();

            DiscountContext.SeedDevelopmentData((pgConfig ?? throw new NullReferenceException(nameof(pgConfig))).Value);
        }
    }
}
