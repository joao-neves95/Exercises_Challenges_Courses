using eShop.Discount.Grpc.Extensions;
//using eShop.Discount.Grpc.Services;
using eShop.Discount.Shared.Data;
using eShop.Discount.Shared.Models.Config;
using eShop.Discount.Shared.Repositories;

namespace eShop.Discount.Grpc
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Additional configuration is required to successfully run gRPC on macOS.
            // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

            builder.Services.Configure<PgsqlConfig>(builder.Configuration.GetSection(PgsqlConfig.KeyName));

            // Add services to the container.
            builder.Services.AddGrpc();

            builder.Services.AddScoped<IDiscountContext, DiscountContext>();
            builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.SeedPgsqlDevelopmentData();
            }

            // Configure the HTTP request pipeline.
            //app.MapGrpcService<GreeterService>();

            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}
