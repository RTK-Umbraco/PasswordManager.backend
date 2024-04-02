using PasswordManager.Users.ApplicationServices.Repositories.User;
using PasswordManager.Users.Domain.User;
using PasswordManager.Users.Infrastructure.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace PasswordManager.Users.Infrastructure.UserRepository;
public class UserRepository : BaseRepository<UserModel, UserEntity>, IUserRepository
{
    public UserRepository(UserContext context) : base(context)
    {
    }

    private DbSet<UserEntity> GetUserDbSet()
    {
        if (Context.Users is null)
            throw new InvalidOperationException("Database configuration not setup correctly");
        return Context.Users;
    }

    protected override UserModel Map(UserEntity entity)
    {
        return UserEntityMapper.Map(entity);
    }

    protected override UserEntity Map(UserModel model)
    {
        return UserEntityMapper.Map(model);
    }
}
