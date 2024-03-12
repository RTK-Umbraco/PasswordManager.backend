using Microsoft.Extensions.DependencyInjection;
using PasswordManager.Users.Infrastructure.Startup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PasswordManager.Users.ApplicationServices.Repositories.Operations;
using PasswordManager.Users.ApplicationServices.Repositories.User;
using PasswordManager.Users.Infrastructure.UserRepository;

namespace PasswordManager.Users.Infrastructure.Installers;

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

        serviceCollection.AddDbContext<UserContext>(options => options.UseSqlServer(connectionString));

        serviceCollection.AddScoped<IUserRepository, UserRepository.UserRepository>();
        serviceCollection.AddScoped<IOperationRepository, OperationRepository.OperationRepository>();
    }
}

