using PasswordManager.KeyVaults.Domain.Operations;

namespace PasswordManager.KeyVaults.ApplicationServices.Repositories.Operations;
public interface IOperationRepository : IBaseRepository<Operation>
{
    Task<Operation?> GetByRequestId(string requestId);
    Task<ICollection<Operation>> GetSecurityKeyOperations(Guid securitykeyId);
}