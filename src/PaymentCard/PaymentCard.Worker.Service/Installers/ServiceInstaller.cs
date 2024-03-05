using PasswordManager.PaymentCard.Infrastructure.Installers;

namespace PasswordManager.PaymentCard.Worker.Service.Installers
{
    public class ServiceInstaller : IDependencyInstaller
    {
        public void Install(IServiceCollection serviceCollection, DependencyInstallerOptions options)
        {
            //Add service dependencies
        }
    }
}