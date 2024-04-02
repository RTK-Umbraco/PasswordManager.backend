using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Users.ApplicationServices.UserPassword.DeleteUserPassword
{
    public class DeleteUserPasswordServiceException : Exception
    {
        public DeleteUserPasswordServiceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
