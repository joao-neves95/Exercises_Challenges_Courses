using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace eShop.ApiGateways.Web
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile($"./ocelot.{builder.Environment.EnvironmentName}.json", true, true);

            // Add services to the container.

            builder.Logging
                .AddConfiguration(builder.Configuration.GetSection("Logging"))
                .AddConsole()
                .AddDebug()
                ;

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services
                .AddOcelot()
                .AddCacheManager(settings => settings.WithDictionaryHandle())
                ;

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Local_K8s")
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.MapControllers();

            await app.UseOcelot();

            app.Run();
        }
    }
}
