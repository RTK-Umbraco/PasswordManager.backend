using PasswordManager.Password.Domain.Operations;
using PasswordManager.Password.Domain.Password;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Password.ApplicationServices.AddPassword
{
    public interface ICreatePasswordService
    {
        Task<OperationResult> RequestCreatePassword(PasswordModel passwordModel, OperationDetails operationDetails);
        Task CreatePassword(PasswordModel passwordModel);
    }
}
