using PasswordManager.Users.Domain.Operations;

namespace PasswordManager.Users.ApplicationServices.UserPassword.DeleteUserPassword
{
    public interface IDeleteUserPasswordService
    {
        Task<OperationResult> RequestDeleteUserPassword(Guid userId, OperationDetails operationDetails);
        Task DeleteUserPassword(Guid userId, string createdByUserId);
    }
}
