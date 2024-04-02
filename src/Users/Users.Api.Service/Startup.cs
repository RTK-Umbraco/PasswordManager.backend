using System.Security.Claims;
using System.Text.Json.Serialization;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using PasswordManager.Users.Api.Service.CurrentUser;
using PasswordManager.Users.Api.Service.Handlers;
using PasswordManager.Users.ApplicationServices.Extensions;
using PasswordManager.Users.Infrastructure.Extensions;

namespace PasswordManager.Users.Api.Service;

public class Startup
{
    public IConfiguration Configuration { get; set; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        //Figure out if AddMvc is used
        services.AddMvc()
            .AddJsonOptions(options =>
            {
                var enumConvertor = new JsonStringEnumConverter();
                options.JsonSerializerOptions.Converters.Add(enumConvertor);
            });

        services.AddApplicationServiceServices();

        services.AddControllers();

        services.AddSwaggerGen(c =>
        {
            c.SupportNonNullableReferenceTypes();
            c.EnableAnnotations();
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Type = SecuritySchemeType.ApiKey,
                Name = "Authorization",
                In = ParameterLocation.Header,
                Scheme = JwtBearerDefaults.AuthenticationScheme,

            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new string[] {}
                }
            });
        });
        services.AddSingleton(FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.FromJson(Configuration[Infrastructure.Constants.ConfigurationKeys.FirebaseProjectCredentials]),
        }));
        services.AddHttpContextAccessor();
        services.AddScoped<ClaimsPrincipal>(p => p.GetRequiredService<IHttpContextAccessor>().HttpContext?.User);
        services
            .AddAuthentication("FirebaseUser")
            .AddScheme<AuthenticationSchemeOptions, FirebaseUserAuthenticationHandler>("FirebaseUser",
                (o) =>
                {

                });
        services.AddTransient<ICurrentUser, CurrentUser.CurrentUser>();

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment() || env.IsStaging())
            app.UseDeveloperExceptionPage();

        app.UseSwagger();

        if (env.IsDevelopment() || env.IsStaging() || Infrastructure.Constants.Environment.IsGeneratingApi)
            app.UseDeveloperExceptionPage();

        app.UseSwaggerUI(options =>
        {
            options.RoutePrefix = string.Empty;
            options.SwaggerEndpoint(Constants.Routes.SwaggerEndpoint, Constants.Services.ApiName);
        });

        app.UseRouting();
        
        app.UseAuthorization();
        app.UseAuthentication();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapSwagger();
        });

        if (env.IsEnvironment("integration-test") || Infrastructure.Constants.Environment.IsGeneratingApi)
            return;

        app.EnsureDatabaseMigrated();
        //If you want to add rebus you need to add it here!
    }
}
