using PasswordManager.Users.Domain.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Users.ApplicationServices.UserPassword.DeleteUserPassword
{
    public interface IDeleteUserPassword
    {
        Task<OperationResult> RequestDeleteUserPassword(Guid userId, OperationDetails operationDetails);
        Task DeleteUserPassword(Guid userId);
    }
}
