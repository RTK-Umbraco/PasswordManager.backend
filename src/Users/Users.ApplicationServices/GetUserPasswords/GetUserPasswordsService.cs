using PasswordManager.Users.ApplicationServices.Components;
using PasswordManager.Users.ApplicationServices.Repositories.User;
using PasswordManager.Users.Domain.User;

namespace PasswordManager.Users.ApplicationServices.GetUserPasswords;
public sealed class GetUserPasswordsService : IGetUserPasswordsService
{
    private readonly IPasswordComponent _passwordComponent;
    private readonly IUserRepository _userRepository;

    public GetUserPasswordsService(IPasswordComponent passwordComponent, IUserRepository userRepository)
    {
        _passwordComponent = passwordComponent;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserPasswordModel>> GetUserPasswordsByUrl(Guid userId, string url)
    {
        //return await _passwordComponent.GetUserPasswords(userId);
        var user = await _userRepository.Get(userId);

        if (user is null)
        {
            throw new GetUserPasswordsServiceException("Could not found user");
        }

        if (user.IsDeleted())
        {
            throw new GetUserPasswordsServiceException("Cannot get user password because the user is marked as deleted");
        }

        var passwords = await _passwordComponent.GetUserPassword(userId, url);

        throw new NotImplementedException();
    }
}
