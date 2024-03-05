using PasswordManager.Password.ApplicationServices.Repositories.Password;
using PasswordManager.Password.Domain.Password;
using PasswordManager.Password.Infrastructure.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace PasswordManager.Password.Infrastructure.PasswordRepository;
public class PasswordRepository : BaseRepository<PasswordModel, PasswordEntity>, IPasswordRepository
{
    public PasswordRepository(PasswordContext context) : base(context)
    {
    }

    private DbSet<PasswordEntity> GetUserDbSet()
    {
        if (Context.Passwords is null)
            throw new InvalidOperationException("Database configuration not setup correctly");
        return Context.Passwords;
    }

    protected override PasswordModel Map(PasswordEntity entity)
    {
        return PasswordEntityMapper.Map(entity);
    }

    protected override PasswordEntity Map(PasswordModel model)
    {
        return PasswordEntityMapper.Map(model);
    }
}
