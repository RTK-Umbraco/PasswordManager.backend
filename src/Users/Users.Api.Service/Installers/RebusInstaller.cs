using PasswordManager.Users.Infrastructure.Installers;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Users.Messages.CreateUserPassword;

namespace PasswordManager.Users.Api.Service.Installers;

public class RebusInstaller : IDependencyInstaller
{
    public void Install(IServiceCollection serviceCollection, DependencyInstallerOptions options)
    {
        // For now skip any further configuration if we are running tests
        if (options.HostEnvironment.IsEnvironment("integration-test")) return;

        var serviceBusConnectionString = "Endpoint=sb://sb-passwordmanager-servicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=w8DGNEpb44MfTiLp+GS18zea9b4UanhYT+ASbJ1fHCY=";

        if (string.IsNullOrWhiteSpace(serviceBusConnectionString))
            throw new InvalidOperationException("Unable to resolve service bus connection string named " +
                                                $"{PasswordManager.Users.Infrastructure.Constants.ConfigurationKeys.ServiceBusConnectionString} " +
                                                "from environment variables");

        serviceCollection.AddRebus((configure, provider) => configure
            .Logging(l => l.MicrosoftExtensionsLogging(provider.GetRequiredService<ILoggerFactory>()))
            .Transport(t => t.UseAzureServiceBusAsOneWayClient(serviceBusConnectionString))
            .Routing(r => r.TypeBased()
                .MapAssemblyOf<CreateUserPasswordCommand>(Infrastructure.Constants.ServiceBus.InputQueue))
        ); ;
    }
}