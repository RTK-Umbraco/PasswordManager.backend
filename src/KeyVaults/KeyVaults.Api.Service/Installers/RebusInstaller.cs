using KeyVaults.Messages.CreateSecurityKey;
using PasswordManager.KeyVaults.Infrastructure.Installers;
using Rebus.Config;
using Rebus.Routing.TypeBased;

namespace PasswordManager.KeyVaults.Api.Service.Installers;

public class RebusInstaller : IDependencyInstaller
{
    public void Install(IServiceCollection serviceCollection, DependencyInstallerOptions options)
    {
        // For now skip any further configuration if we are running tests
        if (options.HostEnvironment.IsEnvironment("integration-test")) return;

        var serviceBusConnectionString =
        options.Configuration[Infrastructure.Constants.ConfigurationKeys.ServiceBusConnectionString];

        if (string.IsNullOrWhiteSpace(serviceBusConnectionString))
            throw new InvalidOperationException("Unable to resolve service bus connection string named " +
                                                $"{PasswordManager.KeyVaults.Infrastructure.Constants.ConfigurationKeys.ServiceBusConnectionString} " +
                                                "from environment variables");

        serviceCollection.AddRebus((configure, provider) => configure
            .Logging(l => l.MicrosoftExtensionsLogging(provider.GetRequiredService<ILoggerFactory>()))
            .Transport(t => t.UseAzureServiceBusAsOneWayClient(serviceBusConnectionString))
            .Routing(r => r.TypeBased()
            .MapAssemblyOf<CreateSecurityKeyCommand>(Infrastructure.Constants.ServiceBus.InputQueue))
        );
    }
}