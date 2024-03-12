using PasswordManager.Password.ApplicationServices.GetPassword;
using PasswordManager.Users.Domain.User;

namespace PasswordManager.Users.ApplicationServices.GetUserPasswords;
internal class GetUserPasswordsService : IGetUserPasswordsService
{
    private readonly IGetPasswordService _getPasswordService;

    public GetUserPasswordsService(IGetPasswordService getPasswordService)
    {
        _getPasswordService = getPasswordService;
    }

    public async Task<IEnumerable<UserPasswordModel>> GetUserPasswords(Guid userId)
    {
        var userPasswords = await _getPasswordService.GetUserPasswords(userId);

        return userPasswords.Select(UserPasswordModelMapper.Map);
    }
}
