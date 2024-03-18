using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Password.Domain.VaultObject.ValueObjects
{
    public class PasswordData
    {
        public string Url { get; private set; }
        public string FriendlyName { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }

        public PasswordData(string url, string friendlyName, string username, string password)
        {
            Url = url;
            FriendlyName = friendlyName;
            Username = username;
            Password = password;
        }
    }
}
