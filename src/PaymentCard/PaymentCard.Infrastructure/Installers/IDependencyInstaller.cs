using Microsoft.Extensions.DependencyInjection;

namespace PasswordManager.PaymentCard.Infrastructure.Installers;
public interface IDependencyInstaller
{
    void Install(IServiceCollection serviceCollection, DependencyInstallerOptions options);
}