using PasswordManager.Password.ApplicationServices.PasswordGenerator;
using PasswordManager.Password.ApplicationServices.Repositories.Password;
using PasswordManager.Password.Domain.Password;

namespace PasswordManager.Password.ApplicationServices.GetPassword;

public class GetPasswordService : IGetPasswordService
{
    private readonly IPasswordRepository _passwordRepository;
    private readonly IGeneratePasswordService _passwordGeneratorService;

    public GetPasswordService(IPasswordRepository passwordRepository, IGeneratePasswordService passwordGeneratorService)
    {
        _passwordRepository = passwordRepository;
        _passwordGeneratorService = passwordGeneratorService;
    }

    public async Task<PasswordModel?> GetPassword(Guid passwordId)
    {
        return await _passwordRepository.Get(passwordId);  
    }

    public async Task<IEnumerable<PasswordModel>> GetPasswords()
    {
        return await _passwordRepository.GetAll();
    }

    public async Task<string> GeneratePassword(Guid userId)
    {
        return await _passwordGeneratorService.GeneratePassword(20);
    }
}
