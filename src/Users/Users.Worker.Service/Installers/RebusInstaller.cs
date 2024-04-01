using Rebus.Bus;
using Rebus.Retry.Simple;
using Rebus.Config;
using PasswordManager.Users.Infrastructure.Installers;

namespace PasswordManager.Users.Worker.Service.Installers
{
    public class RebusInstaller : IDependencyInstaller
    {
        /// <summary>
        /// Events that this worker subscribes to
        /// </summary>
        private static readonly Type[] EventSubscriptionTypes = {
            // typeof(MyFeatureHasHappenedEvents)
        };

        public void Install(IServiceCollection serviceCollection, DependencyInstallerOptions options)
        {
            // Register handlers
            serviceCollection.AutoRegisterHandlersFromAssemblyOf<Program>();

            // Configure Rebus
            var serviceBusConnectionString =
                "Endpoint=sb://sb-passwordmanager-servicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=w8DGNEpb44MfTiLp+GS18zea9b4UanhYT+ASbJ1fHCY=";

            if (string.IsNullOrWhiteSpace(serviceBusConnectionString))
                throw new InvalidOperationException($"Unable to resolve service bus connection string named " +
                                                    $"{Infrastructure.Constants.ConfigurationKeys.ServiceBusConnectionString} " +
                                                    "from environment variables");

            serviceCollection.AddRebus((configure, provider) => configure
                .Logging(l => l.MicrosoftExtensionsLogging(provider.GetRequiredService<ILoggerFactory>()))
                .Transport(t =>
                {
                    t.UseAzureServiceBus(
                            connectionString: serviceBusConnectionString,
                            inputQueueAddress: Constants.ServiceBus.InputQueue)
                        .AutomaticallyRenewPeekLock();
                    t.UseNativeDeadlettering();
                })
                //Routing here. Map command
                //Example --> .MapAssemblyOf<CreateCustomerCommand>(Constants.ServiceBus.InputQueue))
                .Options(o =>
                {
                    o.RetryStrategy(maxDeliveryAttempts: 5);
                }), 
                onCreated: SubscribeToEvents);
        }

        /// <summary>
        /// Subscribe to Rebus Topics
        /// </summary>
        private static async Task SubscribeToEvents(IBus bus)
        {
            foreach (var subscriptionType in EventSubscriptionTypes)
            {
                bus.Advanced.SyncBus.Subscribe(subscriptionType);
            }
        }
    }
}