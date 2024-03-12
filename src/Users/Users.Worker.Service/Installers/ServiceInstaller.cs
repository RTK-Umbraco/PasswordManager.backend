using PasswordManager.Users.Infrastructure.Installers;

namespace PasswordManager.Users.Worker.Service.Installers
{
    public class ServiceInstaller : IDependencyInstaller
    {
        public void Install(IServiceCollection serviceCollection, DependencyInstallerOptions options)
        {
            //Add service dependencies
        }
    }
}