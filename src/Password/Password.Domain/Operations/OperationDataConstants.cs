using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Password.Domain.Operations
{
    public sealed class OperationDataConstants
    {
        #region Create Password
        public static string PasswordCreateUrl => "createPasswordUrl";
        public static string PasswordCreateLabel => "createPasswordLabel";
        public static string PasswordCreateUsername => "createPasswordUsername";
        public static string PasswordCreateKey => "createPasswordKey";
        #endregion

        #region Update Password
        public static string CurrentPasswordUrl => "currentPasswordUrl";
        public static string CurrentPasswordLabel => "currentPasswordLabel";
        public static string CurrentPasswordUsername => "currentPasswordUsername";
        public static string CurrentPasswordKey => "currentPasswordKey";

        public static string NewPasswordUrl => "newPasswordUrl";
        public static string NewPasswordLabel => "newPasswordLabel";
        public static string NewPasswordUsername => "newPasswordUsername";
        public static string NewPasswordKey => "newPasswordKey";
        #endregion
    }
}
