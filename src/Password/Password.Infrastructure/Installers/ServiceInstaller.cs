using PasswordManager.Password.Infrastructure.Startup;
using PasswordManager.Password.Infrastructure.PasswordRepository;

namespace PasswordManager.Password.Infrastructure.Installers;

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

        serviceCollection.AddDbContext<PasswordContext>(options => options.UseSqlServer(connectionString));

        serviceCollection.AddScoped<IPasswordRepository, PasswordRepository.PasswordRepository>();
        serviceCollection.AddScoped<IOperationRepository, OperationRepository.OperationRepository>();
    }
}

