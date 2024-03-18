using PasswordManager.KeyVaults.ApplicationServices.CreateSecurityKey;
using PasswordManager.KeyVaults.ApplicationServices.Extensions;
using PasswordManager.KeyVaults.Infrastructure.Installers;

namespace PasswordManager.KeyVaults.Worker.Service.Installers
{
    public class ServiceInstaller : IDependencyInstaller
    {
        public void Install(IServiceCollection services, DependencyInstallerOptions options)
        {
            services.AddApplicationServiceServices();
        }
    }
}