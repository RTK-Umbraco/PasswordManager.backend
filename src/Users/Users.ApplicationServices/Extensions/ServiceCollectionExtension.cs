using Microsoft.Extensions.DependencyInjection;
using PasswordManager.Users.ApplicationServices.Operations;
using PasswordManager.Users.ApplicationServices.User.GetUser;
using PasswordManager.Users.ApplicationServices.UserPassword.CreateUserPassword;
using PasswordManager.Users.ApplicationServices.UserPassword.GetUserPasswords;

namespace PasswordManager.Users.ApplicationServices.Extensions;
public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplicationServiceServices(this IServiceCollection services)
    {
        //Add application service services
        //Use scoped as method to add services

        services.AddScoped<IGetUserPasswordsService, GetUserPasswordsService>();
        services.AddScoped<IGetUserService, GetUserService>();
        services.AddScoped<ICreateUserPasswordService, CreateUserPasswordService>();
        services.AddScoped<IOperationService, OperationService>();
        
        return services;
    }
}
