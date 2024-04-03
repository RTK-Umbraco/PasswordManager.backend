using PasswordManager.Users.Domain.Operations;
using PasswordManager.Users.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Users.ApplicationServices.UserPassword.UpdateUserPassword
{
    public interface IUpdateUserPasswordService
    {
        Task<OperationResult> RequestUpdateUserPassword(UserPasswordModel userPasswordModel, Guid createdByUserId, OperationDetails operationDetails);
        Task<OperationResult> UpdateUserPassword(UserPasswordModel userPasswordModel);
    }
}
