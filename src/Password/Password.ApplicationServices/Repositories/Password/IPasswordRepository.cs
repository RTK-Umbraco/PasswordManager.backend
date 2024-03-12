using PasswordManager.Password.Domain.Password;

namespace PasswordManager.Password.ApplicationServices.Repositories.Password;
public interface IPasswordRepository : IBaseRepository<PasswordModel>
{
    Task UpdatePassword(PasswordModel passwordModel);
    Task<IEnumerable<PasswordModel>> GetUserPasswords(Guid userId);
}
