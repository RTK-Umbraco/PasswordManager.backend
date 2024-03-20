using PasswordManager.Users.Domain.Operations;
using PasswordManager.Users.Domain.User;

namespace PasswordManager.Users.ApplicationServices.CreateUserPassword;
public interface ICreateUserPasswordService
{
    Task<OperationResult> RequestCreateUserPassword(UserPasswordModel userPasswordModel, OperationDetails operationDetails);
    Task CreateUserPassword(UserPasswordModel userPasswordModel);
}
