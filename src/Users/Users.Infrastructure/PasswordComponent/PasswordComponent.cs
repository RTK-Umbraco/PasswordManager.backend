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

    public async  Task<IEnumerable<PasswordModel>> GetUserPasswords(Guid userId)
    {
        var userPasswords = await _passwordRepository.GetUserPasswords(userId);

        return userPasswords.Select(PasswordModelMapper.Map);
    }
}
