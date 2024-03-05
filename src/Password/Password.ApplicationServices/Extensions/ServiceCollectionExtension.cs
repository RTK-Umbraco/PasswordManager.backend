using Microsoft.Extensions.DependencyInjection;
using PasswordManager.Password.ApplicationServices.GetPassword;

namespace PasswordManager.Password.ApplicationServices.Extensions;
public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplicationServiceServices(this IServiceCollection services)
    {
        //Add application service services
        //Use scoped as method to add services
        services.AddScoped<IGetPasswordService, GetPasswordService>();

        return services;
    }
}
