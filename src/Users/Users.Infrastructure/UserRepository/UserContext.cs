using PasswordManager.Users.Infrastructure.OperationRepository;
using Microsoft.EntityFrameworkCore;

namespace PasswordManager.Users.Infrastructure.UserRepository;
public class UserContext : DbContext
{
    public UserContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<OperationEntity>? UsersOperations { get; set; }
    public DbSet<UserEntity>? Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new OperationEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}
