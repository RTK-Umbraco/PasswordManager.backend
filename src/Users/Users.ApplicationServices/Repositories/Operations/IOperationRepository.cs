using PasswordManager.Users.Domain.Operations;

namespace PasswordManager.Users.ApplicationServices.Repositories.Operations;
public interface IOperationRepository : IBaseRepository<Operation>
{
    Task<Operation?> GetByRequestId(string requestId);
    Task<ICollection<Operation>> GetUserOperations(Guid userId);
}