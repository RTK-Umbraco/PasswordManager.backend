using Microsoft.Extensions.DependencyInjection;
using PasswordManager.PaymentCard.Infrastructure.Startup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PasswordManager.PaymentCard.ApplicationServices.Repositories.Operations;
using PasswordManager.PaymentCard.ApplicationServices.Repositories.PaymentCard;
using PasswordManager.PaymentCard.Infrastructure.PaymentCardRepository;

namespace PasswordManager.PaymentCard.Infrastructure.Installers;

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

        serviceCollection.AddDbContext<PaymentCardContext>(options => options.UseSqlServer(connectionString));

        serviceCollection.AddScoped<IPaymentCardRepository, PaymentCardRepository.PaymentCardRepository>();
        serviceCollection.AddScoped<IOperationRepository, OperationRepository.OperationRepository>();
    }
}

