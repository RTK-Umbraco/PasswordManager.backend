using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Password.Domain.Password
{
    internal class PasswordSecurityModel : BaseModel
    {
        public string Key { get; }

        public PasswordSecurityModel(Guid id, DateTime createdUtc, DateTime modifiedUtc, bool deleted, string key) : base(id, createdUtc, modifiedUtc, deleted)
        {
            Key = key;
        }

        public PasswordSecurityModel(Guid id, string key) : base(id)
        {
            Key = key;
        }

        public static PasswordSecurityModel CreatePasswordSecurity(string key)
        {
            return new PasswordSecurityModel(Guid.NewGuid(), key);
        }

        public static PasswordSecurityModel UpdatePasswordSecurity(Guid id, string key)
        {
            return new PasswordSecurityModel(id, key);
        }
    }
}
