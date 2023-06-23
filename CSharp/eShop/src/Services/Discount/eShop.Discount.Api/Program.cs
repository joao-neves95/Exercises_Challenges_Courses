using eShop.Discount.Api.Extensions;
using eShop.Discount.Shared.Data;
using eShop.Discount.Shared.Models.Config;
using eShop.Discount.Shared.Repositories;

namespace eShop.Discount.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.Configure<PgsqlConfig>(builder.Configuration.GetSection(PgsqlConfig.KeyName));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IDiscountContext, DiscountContext>();
            builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                app.SeedPgsqlDevelopmentData();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
