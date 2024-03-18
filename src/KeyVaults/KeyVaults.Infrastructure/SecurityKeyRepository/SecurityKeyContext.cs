using PasswordManager.KeyVaults.Infrastructure.OperationRepository;
using Microsoft.EntityFrameworkCore;

namespace PasswordManager.KeyVaults.Infrastructure.SecurityKeyRepository;
public class SecurityKeyContext : DbContext
{
    public SecurityKeyContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<OperationEntity> Operations { get; set; }
    public DbSet<SecurityKeyEntity> SecurityKeys { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new OperationEntityConfiguration());
        modelBuilder.ApplyConfiguration(new SecurityKeyConfiguration());
    }
}
