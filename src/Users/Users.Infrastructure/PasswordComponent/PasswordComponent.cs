using PasswordManager.Password.ApplicationServices.Repositories.Password;
using PasswordManager.Users.ApplicationServices.Components;
using PasswordManager.Users.Domain.User;

namespace PasswordManager.Users.Infrastructure.PasswordComponent;
public sealed class PasswordComponent : IPasswordComponent
{
    private readonly IPasswordRepository _passwordRepository;

    public PasswordComponent(IPasswordRepository passwordRepository)
    {
        _passwordRepository = passwordRepository;
    }

    public async  Task<UserPasswordModel> CreateUserPassword(UserPasswordModel userPasswordModel)
    {
        var passwordModel = await _passwordRepository.Add(UserPasswordModelMapper.Map(userPasswordModel));

        return UserPasswordModelMapper.Map(passwordModel);
    }

    public async  Task<IEnumerable<UserPasswordModel>> GetUserPasswords(Guid userId)
    {
        var userPasswords = await _passwordRepository.GetUserPasswords(userId);

        return userPasswords.Select(UserPasswordModelMapper.Map);
    }
}
