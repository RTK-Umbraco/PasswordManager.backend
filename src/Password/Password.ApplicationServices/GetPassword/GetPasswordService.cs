using PasswordManager.Password.ApplicationServices.PasswordGenerator;
using PasswordManager.Password.ApplicationServices.Repositories.Password;
using PasswordManager.Password.Domain.Password;

namespace PasswordManager.Password.ApplicationServices.GetPassword;

public class GetPasswordService : IGetPasswordService
{
    private readonly IPasswordRepository _passwordRepository;

    public GetPasswordService(IPasswordRepository passwordRepository)
    {
        _passwordRepository = passwordRepository;
    }

    public async Task<PasswordModel?> GetPassword(Guid passwordId)
    {
        return await _passwordRepository.Get(passwordId);  
    }

    public async Task<IEnumerable<PasswordModel>> GetPasswords()
    {
        return await _passwordRepository.GetAll();
    }

    public async Task<IEnumerable<PasswordModel>> GetUserPasswords(Guid userId)
    {
        return await _passwordRepository.GetUserPasswords(userId);
    }
}
