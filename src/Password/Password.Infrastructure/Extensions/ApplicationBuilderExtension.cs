using PasswordManager.Password.Infrastructure.PasswordRepository;

namespace PasswordManager.Password.Infrastructure.Extensions;
public static class ApplicationBuilderExtension
{
    public static void EnsureDatabaseMigrated(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<PasswordContext>();

        context.Database.Migrate();
    }
}