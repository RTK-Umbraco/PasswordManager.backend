using PasswordManager.Users.Domain.Operations;

namespace PasswordManager.Users.ApplicationServices.UserPassword.DeleteUserPassword
{
    public interface IDeleteUserPasswordService
    {
        Task<OperationResult> RequestDeleteUserPassword(Guid passwordId, OperationDetails operationDetails);
        Task DeleteUserPassword(Guid passwordId, string createdByUserId);
    }
}
