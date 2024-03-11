using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Password.ApplicationServices.PasswordGenerator
{
    public interface IGeneratePasswordService
    {
        Task<string> GeneratePassword(int length);
    }
}
