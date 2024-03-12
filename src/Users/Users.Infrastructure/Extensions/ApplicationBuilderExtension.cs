using PasswordManager.Users.Infrastructure.UserRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PasswordManager.Users.Infrastructure.Extensions;
public static class ApplicationBuilderExtension
{
    public static void EnsureDatabaseMigrated(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<UserContext>();

        context.Database.Migrate();
    }
}