using PasswordManager.KeyVaults.Infrastructure.SecurityKeyRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PasswordManager.KeyVaults.Infrastructure.Extensions;
public static class ApplicationBuilderExtension
{
    public static void EnsureDatabaseMigrated(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<SecurityKeyContext>();

        context.Database.Migrate();
    }
}