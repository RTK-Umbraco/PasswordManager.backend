using PasswordManager.Users.Domain.Operations;

namespace PasswordManager.Users.ApplicationServices.Repositories.Operations;

public interface IOperationService
{
    Task<Operation> QueueOperation(Operation operation);
    Task<Operation?> GetOperationByRequestId(string requestId);
    Task<Operation?> UpdateOperationStatus(string requestId, OperationStatus operationStatus);
    Task<ICollection<Operation>> GetUserOperations(Guid UserId);
}