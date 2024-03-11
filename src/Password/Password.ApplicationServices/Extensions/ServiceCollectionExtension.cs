using PasswordManager.Password.ApplicationServices.CreatePassword;
using PasswordManager.Password.ApplicationServices.GetPassword;
using PasswordManager.Password.ApplicationServices.Operations;
using PasswordManager.Password.ApplicationServices.PasswordGenerator;
using PasswordManager.Password.ApplicationServices.UpdatePassword;

namespace PasswordManager.Password.ApplicationServices.Extensions;
public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplicationServiceServices(this IServiceCollection services)
    {
        //Add application service services
        //Use scoped as method to add services
        services.AddScoped<IGetPasswordService, GetPasswordService>();
        services.AddScoped<ICreatePasswordService, CreatePasswordService>();
        services.AddScoped<IOperationService, OperationService>();
        services.AddScoped<IUpdatePasswordService, UpdatePasswordService>();
        services.AddScoped<IGeneratePasswordService, GeneratePasswordService>();

        return services;
    }
}
