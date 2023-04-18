using GamingApi.WebApi.Core.Interfaces;
using GamingApi.WebApi.Core.Interfaces.Services;
using GamingApi.WebApi.Core.Interfaces.Stores;
using GamingApi.WebApi.Core.Services;
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
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Yld Gaming API", Version = "v1" });
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "YldGamingApi.xml"));
        });

        services.AddHttpContextAccessor();

        RegisterMappers(services);
        RegisterServices(services);
        RegisterStores(services);
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
        services.AddSingleton<IGameService<GamesResponse>, GameService>();
    }

    private static void RegisterStores(IServiceCollection services)
    {
        services.AddSingleton<IGamesStore<DataGame>, GameStoreAwsClient>();
    }
}
