using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Users.ApplicationServices.UserPassword.UpdateUserPassword
{
    public class UpdateUserPasswordServiceException : Exception
    {
        public UpdateUserPasswordServiceException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
