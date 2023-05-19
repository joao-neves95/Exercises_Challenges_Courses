using eShop.Basket.Api.Models;
using eShop.Basket.Api.Repositories;

namespace eShop.Basket.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddStackExchangeRedisCache(options =>
                options.Configuration = builder.Configuration.GetValue<string>(
                    $"{CacheConfig.KeyName}:{nameof(CacheConfig.ConnectionString)}"));

            builder.Services.AddScoped<IBasketRepository, BasketRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
