using PasswordManager.Password.Domain.Password;

namespace PasswordManager.Password.ApplicationServices.GetPassword
{
    public interface IGetPasswordService
    {
        Task<PasswordModel?> GetPassword(Guid passwordId);
        Task<IEnumerable<PasswordModel>> GetPasswords();
        Task<IEnumerable<PasswordModel>> GetUserPasswords(Guid userId);
    }
}
