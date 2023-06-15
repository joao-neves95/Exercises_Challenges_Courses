using eShop.Ordering.Api.Extensions;
using eShop.Ordering.Api.EventBusConsumers;
using eShop.Ordering.Api.Models;
using eShop.Ordering.Application.Behaviors;
using eShop.Ordering.Application.Contracts.Infrastructure;
using eShop.Ordering.Application.Contracts.Persistence;
using eShop.Ordering.Application.Features.Order.Commands.CheckoutOrder;
using eShop.Ordering.Application.Mappings;
using eShop.Ordering.Application.Models;
using eShop.Ordering.Infrastructure.Persistence;
using eShop.Ordering.Infrastructure.Repositories;
using eShop.Ordering.Infrastructure.Services;
using eShop.Shared.EventBus.Messages.Constants;

using Microsoft.EntityFrameworkCore;
using System.Reflection;

using FluentValidation;
using MassTransit;

namespace eShop.Ordering.Api
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection(EmailConfig.KeyName));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));

            builder.Services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(CheckoutOrderCommandValidator)));

            builder.Services.AddMediatR(options =>
            {
                options.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
                options.AddOpenBehavior(typeof(ValidationBehaviour<,>));

                options.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(CheckoutOrderCommand)));
            });

            builder.Services.AddMassTransit(options =>
            {
                options.AddConsumer<BasketCheckoutConsumer>();

                options.UsingRabbitMq((ctx, options) =>
                {
                    options.Host(builder.Configuration.GetValue<string>(
                        $"{EventBusConfig.KeyName}:{nameof(EventBusConfig.HostAddress)}"));

                    options.ReceiveEndpoint(
                        EventBusQueueNames.BasketCheckout,
                        options => options.ConfigureConsumer<BasketCheckoutConsumer>(ctx));
                });
            });

            builder.Services.AddDbContext<OrderContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("SqlServerConnectionString")));

            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IEmailService, EmailService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();

            await app.MigrateDatabaseAsync();
            await app.RunAsync();
        }
    }
}
