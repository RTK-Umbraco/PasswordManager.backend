using PasswordManager.Users.ApplicationServices.Components;
using PasswordManager.Users.ApplicationServices.Repositories.User;
using PasswordManager.Users.Domain.User;

namespace PasswordManager.Users.ApplicationServices.GetUserPasswords;
public sealed class GetUserPasswordsService : IGetUserPasswordsService
{
    private readonly IPasswordComponent _passwordComponent;
    private readonly IKeyVaultComponent _KeyVaultComponent;
    private readonly IUserRepository _userRepository;

    public GetUserPasswordsService(IPasswordComponent passwordComponent, IUserRepository userRepository, IKeyVaultComponent keyVaultComponent)
    {
        _passwordComponent = passwordComponent;
        _userRepository = userRepository;
        _KeyVaultComponent = keyVaultComponent;
    }

    public async Task<IEnumerable<UserPasswordModel>> GetUserPasswordsByUrl(Guid userId, string url)
    {
        var user = await _userRepository.Get(userId);

        if (user is null)
        {
            throw new GetUserPasswordsServiceException("Could not found user");
        }

        if (user.IsDeleted())
        {
            throw new GetUserPasswordsServiceException("Cannot get user password because the user is marked as deleted");
        }

        var encryptedPasswords = await _passwordComponent.GetUserPassword(userId, url);

        var decryptedPassword = await _KeyVaultComponent.DecryptPasswords(encryptedPasswords, user.SecretKey);

        return decryptedPassword;
    }
}
