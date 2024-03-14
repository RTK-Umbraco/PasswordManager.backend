using Microsoft.Extensions.DependencyInjection;
using PasswordManager.KeyVaults.Infrastructure.Startup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PasswordManager.KeyVaults.ApplicationServices.Repositories.Operations;
using PasswordManager.KeyVaults.ApplicationServices.Repositories.SecurityKey;
using PasswordManager.KeyVaults.Infrastructure.SecurityKeyRepository;

namespace PasswordManager.KeyVaults.Infrastructure.Installers;

public sealed class ServiceInstaller : IDependencyInstaller
{
    public void Install(IServiceCollection serviceCollection, DependencyInstallerOptions options)
    {
        serviceCollection.AddTransient<IRunOnStartupExecution, RunOnStartupExecution>();
        AddRepositories(serviceCollection, options.Configuration);
    }

    private static void AddRepositories(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var connectionString = configuration[Constants.ConfigurationKeys.SqlDbConnectionString];

        serviceCollection.AddDbContext<SecurityKeyContext>(options => options.UseSqlServer(connectionString));

        serviceCollection.AddScoped<ISecurityKeyRepository, SecurityKeyRepository.SecurityKeyRepository>();
        serviceCollection.AddScoped<IOperationRepository, OperationRepository.OperationRepository>();
    }
}

