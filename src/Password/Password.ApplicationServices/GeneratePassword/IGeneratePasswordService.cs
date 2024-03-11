namespace PasswordManager.Password.ApplicationServices.PasswordGenerator
{
    public interface IGeneratePasswordService
    {
        Task<string> GeneratePassword(int length);
    }
}
