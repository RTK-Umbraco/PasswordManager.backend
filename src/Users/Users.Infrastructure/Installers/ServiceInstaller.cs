using PasswordManager.Users.Infrastructure.Startup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PasswordManager.Users.ApplicationServices.Repositories.Operations;
using PasswordManager.Users.ApplicationServices.Repositories.User;
using PasswordManager.Users.Infrastructure.UserRepository;
using PasswordManager.Users.ApplicationServices.Components;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cloud.Passwordmanager.Password.Api.Client;

namespace PasswordManager.Users.Infrastructure.Installers;

public sealed class ServiceInstaller : IDependencyInstaller
{
    public void Install(IServiceCollection serviceCollection, DependencyInstallerOptions options)
    {
        serviceCollection.AddTransient<IRunOnStartupExecution, RunOnStartupExecution>();
        AddRepositories(serviceCollection, options.Configuration);
        AddComponents(serviceCollection, options.Configuration);
    }

    private static void AddRepositories(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var connectionString = configuration[Constants.ConfigurationKeys.SqlDbConnectionString];

        serviceCollection.AddDbContext<UserContext>(options => options.UseSqlServer(connectionString));

        serviceCollection.AddScoped<IUserRepository, UserRepository.UserRepository>();
        serviceCollection.AddScoped<IOperationRepository, OperationRepository.OperationRepository>();
    }

    private static void AddComponents(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        SetupPasswordIntegration(serviceCollection, configuration);
        serviceCollection.AddScoped<IPasswordComponent, PasswordComponent.PasswordComponent>();
    }

    private static void SetupPasswordIntegration(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var httpClientName = Constants.HttpClientNames.Password;

        serviceCollection.AddHttpClient(httpClientName, c => { c.BaseAddress = new Uri("http://localhost:62600"); });

        serviceCollection.AddTransient<IPasswordmanagerPasswordApiClient, PasswordmanagerPasswordApiClient>(c =>
        {
            var clientFactory = c.GetRequiredService<IHttpClientFactory>();
            var client = clientFactory.CreateClient(httpClientName);

            return new PasswordmanagerPasswordApiClient(string.Empty, client);
        });
    }
}

