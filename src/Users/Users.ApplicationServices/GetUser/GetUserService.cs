using PasswordManager.Users.ApplicationServices.Repositories.User;
using PasswordManager.Users.Domain.User;

namespace PasswordManager.Users.ApplicationServices.GetUser;
internal class GetUserService : IGetUserService
{
    private readonly IUserRepository _userRepository;

    public GetUserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserModel?> GetUser(Guid userId)
    {
        var user = await _userRepository.Get(userId);

        return user;
    }
}
