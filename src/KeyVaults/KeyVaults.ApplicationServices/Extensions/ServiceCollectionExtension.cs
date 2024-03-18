using Microsoft.Extensions.DependencyInjection;
using PasswordManager.KeyVaults.ApplicationServices.CreateSecurityKey;
using PasswordManager.KeyVaults.ApplicationServices.DeleteSecurityKey;
using PasswordManager.KeyVaults.ApplicationServices.KeyVaultManager;
using PasswordManager.KeyVaults.ApplicationServices.Operations;
using PasswordManager.KeyVaults.ApplicationServices.Protection;
using PasswordManager.KeyVaults.ApplicationServices.UpdateSecurityKey;

namespace PasswordManager.KeyVaults.ApplicationServices.Extensions;
public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplicationServiceServices(this IServiceCollection services)
    {
        //Add application service services
        //Use scoped as method to add services
        services.AddScoped<IProtectionService, ProtectionService>();
        services.AddScoped<IOperationService, OperationService>();
        services.AddScoped<ICreateSecurityKeyService, CreateSecurityKeyService>();
        services.AddScoped<IUpdateSecurityKeyService, UpdateSecurityKeyService>();
        services.AddScoped<IDeleteSecurityKeyService, DeleteSecurityKeyService>();
        services.AddScoped<IKeyVaultManagerService, KeyVaultManagerService>();

        return services;
    }
}
