using Microsoft.Extensions.DependencyInjection;

namespace PasswordManager.KeyVaults.Infrastructure.Installers;
public interface IDependencyInstaller
{
    void Install(IServiceCollection serviceCollection, DependencyInstallerOptions options);
}