namespace PasswordManager.Password.ApplicationServices.CreatePassword;

public interface ICreatePasswordService
{
    Task<OperationResult> RequestCreatePassword(PasswordModel passwordModel, OperationDetails operationDetails);
    Task CreatePassword(PasswordModel passwordModel);
}
