using PasswordManager.Users.Domain;

namespace PasswordManager.Users.ApplicationServices.Repositories;
public interface IBaseRepository<T> where T : BaseModel
{
    Task<T?> Get(Guid id);
    Task<ICollection<T>> GetAll();
    Task<T> Upsert(T baseModel);
}
