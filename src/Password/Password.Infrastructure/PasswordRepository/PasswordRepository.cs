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

    public async Task UpdatePassword(PasswordModel passwordModel)
    {
        var updatePasswordModel = await Get(passwordModel.Id);
        if (updatePasswordModel == null)
        {
            throw new PasswordRepositoryException($"Could not update entity {typeof(PasswordEntity)} - could not find by Id: {passwordModel.Id}");
        }

        await Upsert(passwordModel);
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
