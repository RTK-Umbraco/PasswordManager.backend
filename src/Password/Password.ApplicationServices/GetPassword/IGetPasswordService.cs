using PasswordManager.Password.Domain.Password;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Password.ApplicationServices.GetPassword
{
    public interface IGetPasswordService
    {
        Task<PasswordModel?> GetPassword(Guid passwordId);
        Task<IEnumerable<PasswordModel>> GetPasswords();
    }
}
