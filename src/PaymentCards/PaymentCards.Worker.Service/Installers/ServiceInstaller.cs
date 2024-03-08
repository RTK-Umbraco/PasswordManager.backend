using PasswordManager.PaymentCards.Infrastructure.Installers;

namespace PasswordManager.PaymentCards.Worker.Service.Installers
{
    public class ServiceInstaller : IDependencyInstaller
    {
        public void Install(IServiceCollection serviceCollection, DependencyInstallerOptions options)
        {
            //Add service dependencies
        }
    }
}