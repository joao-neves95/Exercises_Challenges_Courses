using GamingApi.WebApi.Contracts.Config;
using GamingApi.WebApi.Contracts.Constants;
using GamingApi.WebApi.Contracts.Interfaces;
using GamingApi.WebApi.Contracts.Interfaces.Services;
using GamingApi.WebApi.Contracts.Interfaces.Stores;
using GamingApi.WebApi.Core.Services;
using GamingApi.WebApi.Infrastructure;
using GamingApi.WebApi.Infrastructure.Entities;
using GamingApi.WebApi.Infrastructure.Mappers;
using GamingApi.WebApi.Infrastructure.Network;

using Microsoft.AspNetCore.Rewrite;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerUI;

using Yld.GamingApi.WebApi.ApiContracts;

namespace Yld.GamingApi.WebApi;

public sealed class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration.ThrowIfNull();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<YldConfig>(_configuration.GetSection(AppSettingsKeys.YldConfig).ThrowIfNull());
        services.Configure<AppConfig>(_configuration.GetSection(AppSettingsKeys.AppConfig).ThrowIfNull());

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Yld Gaming API", Version = "v1" });
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "YldGamingApi.xml"));
        });

        services.AddHttpContextAccessor();

        RegisterMappers(services);
        RegisterServices(services);
        RegisterInfrastructureServices(services);
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Yld Gaming API v1");
            c.DocExpansion(DocExpansion.List);
        });

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger"));

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    private static void RegisterMappers(IServiceCollection services)
    {
        // We use hand-made mappers, instead something like AutoMapper because they are faster, more flexible and "strongly typed".
        services.AddSingleton<IMapper<DataGame, GameResponse>, DataGameToApiMapper>();
    }

    private static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<IGameService<GamesResponse>, GameService>();
    }

    private static void RegisterInfrastructureServices(IServiceCollection services)
    {
        //services.AddScoped<IProxyHttpClient, DotnetHttpClient>();
        services
            .AddHttpClient<IProxyHttpClient, DotnetHttpClient>()
            .SetHandlerLifetime(TimeSpan.FromDays(5));

        services.AddSingleton<IProxyJsonClient, NewtonsoftJsonClient>();

        services.AddScoped<IGamesStore<DataGame>, GameStoreAwsClient>();
    }
}
