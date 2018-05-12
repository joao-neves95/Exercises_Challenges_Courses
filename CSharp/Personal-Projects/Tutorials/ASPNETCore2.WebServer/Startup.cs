using System;
using System.Text;
using OwaspHeaders.Core.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using WebServer.Services;
using WebServer.Services.Data;
using Microsoft.AspNetCore.Http;

namespace WebServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            DotNetEnv.Env.Load();
            services.AddDbContext<MySqlDbContext>();
            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<MySqlDbContext>()
                    .AddDefaultTokenProviders();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // remove default claims.
            services.AddAuthentication(options => 
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; // For development.
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    SaveSigninToken = true,
                    // Clock skew compensates for server time drift.
                    ClockSkew = TimeSpan.FromMinutes(1),
                    //Specify the key used to sign the token:
                    //IssuerSigningKey = SecurityKey,
                    RequireSignedTokens = true,
                    //Ensure the token hasn't expired:
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    //Ensure the token audience matches our audience value(default true):
                    ValidateAudience = true,
                    ValidAudience = DotNetEnv.Env.GetString("JWT_ISSUER"),
                    //Ensure the token was issued by a trusted authorization server(default true):
                    ValidateIssuer = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(DotNetEnv.Env.GetString("JWT_KEY"))),
                    ValidIssuer = DotNetEnv.Env.GetString("JWT_ISSUER")
                };
            });
            services.AddLogging();
            services.AddMvc()
                    .AddJsonOptions(options => 
                    {
                        options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
                        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, MySqlDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSecureHeadersMiddleware(Security.SecureHeadersConfiguration());

            app.UseCors(builder =>
            {
                builder.WithOrigins("http://localhost:3000", "http://localhost:5000", "http://localhost:49642", "http://localhost")
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials();
            });

            app.UseAuthentication();
            app.UseStaticFiles();

            app.Use(async (context, _next) =>
            {
                await _next();
                Console.WriteLine(context.Response.StatusCode);

                if (context.Response.StatusCode == StatusCodes.Status401Unauthorized || context.Response.StatusCode == StatusCodes.Status403Forbidden)
                {
                    context.Response.Redirect("/");
                    return;
                }

            });

            app.UseMvc();

            // ===== Create the Identity tables ======
            dbContext.Database.EnsureCreated();
        }
    }
}
