using Microsoft.Extensions.DependencyInjection;
using PasswordManager.Password.ApplicationServices.Repositories.Password;
using PasswordManager.Password.Infrastructure.PasswordRepository;
using PasswordManager.Users.ApplicationServices.GetUserPasswords;

namespace PasswordManager.Users.ApplicationServices.Extensions;
public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplicationServiceServices(this IServiceCollection services)
    {
        //Add application service services
        //Use scoped as method to add services

        services.AddScoped<IGetUserPasswordsService, GetUserPasswordsService>();
        services.AddScoped<IPasswordRepository, PasswordRepository>();
        return services;
    }
}
