using PasswordManager.Users.ApplicationServices.Operations;
using PasswordManager.Users.ApplicationServices.UserPassword.CreateUserPassword;
using PasswordManager.Users.ApplicationServices.UserPassword.DeleteUserPassword;
using PasswordManager.Users.ApplicationServices.UserPassword.UpdateUserPassword;
using PasswordManager.Users.Infrastructure.Installers;

namespace PasswordManager.Users.Worker.Service.Installers
{
    public class ServiceInstaller : IDependencyInstaller
    {
        public void Install(IServiceCollection serviceCollection, DependencyInstallerOptions options)
        {
            serviceCollection.AddScoped<IOperationService, OperationService>();
            serviceCollection.AddScoped<ICreateUserPasswordService, CreateUserPasswordService>();
            serviceCollection.AddScoped<IDeleteUserPasswordService, DeleteUserPasswordService>();
            serviceCollection.AddScoped<IUpdateUserPasswordService, UpdateUserPasswordService>();
        }
    }
}