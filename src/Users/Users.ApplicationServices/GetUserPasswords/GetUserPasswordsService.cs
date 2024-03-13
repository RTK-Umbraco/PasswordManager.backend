using PasswordManager.Users.ApplicationServices.Components;
using PasswordManager.Users.Domain.User;

namespace PasswordManager.Users.ApplicationServices.GetUserPasswords;
public sealed class GetUserPasswordsService : IGetUserPasswordsService
{
    private readonly IPasswordComponent _passwordComponent;

    public GetUserPasswordsService(IPasswordComponent passwordComponent)
    {
        _passwordComponent = passwordComponent;
    }

    public async Task<IEnumerable<PasswordModel>> GetUserPasswords(Guid userId)
    {
        return await _passwordComponent.GetUserPasswords(userId);
    }
}
