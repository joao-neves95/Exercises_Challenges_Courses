using Microsoft.AspNetCore.Rewrite;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

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
}
