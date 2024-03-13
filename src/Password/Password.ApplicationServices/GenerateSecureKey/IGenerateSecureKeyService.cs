namespace PasswordManager.Password.ApplicationServices.PasswordGenerator
{
    public interface IGenerateSecureKeyService
    {
        Task<string> GeneratePassword(int length);
    }
}
