using PasswordManager.Users.Infrastructure.UserRepository;
using System.Security.Claims;
using System.Security.Cryptography;

namespace PasswordManager.Users.Api.Service.CurrentUser
{
    public class CurrentUser : ICurrentUser
    {
        private readonly ClaimsPrincipal _claimsPrincipal;
        private readonly UserContext _context;

        public CurrentUser(ClaimsPrincipal claimsPrincipal, UserContext context)
        {
            _claimsPrincipal = claimsPrincipal;
            _context = context;
        }
        private UserEntity currentUser { get; set; }
        public UserEntity GetUser()
        {
            if (currentUser == null)
            {
                var user = this._context.Users!.FirstOrDefault(u => u.FirebaseId == _claimsPrincipal.FindFirstValue("id"));

                if (user == null)
                {
                    user = new UserEntity(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow,
                        _claimsPrincipal.FindFirstValue("id"), "test");
                    _context.Users!.Add(user);

                    _context.SaveChanges();
                }

                currentUser = user;
            }

            return currentUser;

        }
        public string GenerateSecretKey(int bitLength)
        {
            if (bitLength != 128 && bitLength != 192 && bitLength != 256)
            {
                throw new ArgumentException("AES key length must be 128, 192, or 256 bits.");
            }

            int byteLength = bitLength / 8;
            byte[] keyBytes = GenerateRandomBytes(byteLength);
            return Convert.ToBase64String(keyBytes);
        }

        private byte[] GenerateRandomBytes(int length)
        {
            byte[] randomBytes = new byte[length];
            RandomNumberGenerator.Create().GetBytes(randomBytes);
            return randomBytes;
        }

    }
}
