using PasswordManager.KeyVaults.Domain.Operations;

namespace PasswordManager.KeyVaults.ApplicationServices.Operations;
public interface IOperationService
{
    Task<Operation> QueueOperation(Operation operation);
    Task<Operation?> GetOperationByRequestId(string requestId);
    Task<Operation?> UpdateOperationStatus(string requestId, OperationStatus operationStatus);
    Task<ICollection<Operation>> GetSecurityKeyOperations(Guid UserId);
}
