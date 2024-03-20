using PasswordManager.Users.ApplicationServices.CreateUserPassword;
using PasswordManager.Users.ApplicationServices.Operations;
using PasswordManager.Users.Infrastructure.Installers;

namespace PasswordManager.Users.Worker.Service.Installers
{
    public class ServiceInstaller : IDependencyInstaller
    {
        public void Install(IServiceCollection serviceCollection, DependencyInstallerOptions options)
        {
            serviceCollection.AddScoped<ICreateUserPasswordService, CreateUserPasswordService>();
            serviceCollection.AddScoped<IOperationService, OperationService>();
            //Add service dependencies
        }
    }
}