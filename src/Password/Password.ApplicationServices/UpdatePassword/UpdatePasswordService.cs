using PasswordManager.Password.Domain.Operations;
using PasswordManager.Password.Domain.Password;

namespace PasswordManager.Password.ApplicationServices.UpdatePassword
{
    public class UpdatePasswordService : IUpdatePasswordService
    {
        public Task<OperationResult> RequestUpdatePassword(PasswordModel updatePasswordModel, OperationDetails operationDetails)
        {
            throw new NotImplementedException();
        }
    }
}
