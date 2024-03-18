using System.Security.Cryptography;

namespace PasswordManager.KeyVaults.ApplicationServices.Protection
{
    public class ProtectionService : IProtectionService
    {
        public string Protect(string plainText, string key)
        {
            // Converts from base64 string to byte array
            byte[] keyBytes = Convert.FromBase64String(key);

            using (var aes = Aes.Create())
            {
                if (aes.ValidKeySize(keyBytes.Length * 8) == false)
                {
                    throw new ArgumentException("Invalid key length", nameof(key));
                }

                aes.Key = keyBytes;
                aes.GenerateIV();

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                byte[] encryptedBytes;

                // Encrypt the bytes
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (var sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                    }
                    encryptedBytes = ms.ToArray();
                }

                // Concatenate the IV with the encrypted bytes
                byte[] encryptedBytesWithIv = new byte[aes.IV.Length + encryptedBytes.Length];

                // 0 is used to indicate the position to start copying the array
                Array.Copy(aes.IV, encryptedBytesWithIv, aes.IV.Length);
                Array.Copy(encryptedBytes, 0, encryptedBytesWithIv, aes.IV.Length, encryptedBytes.Length);

                return Convert.ToBase64String(encryptedBytesWithIv);
            }
        }

        public string Unprotect(string protectedText, string key)
        {

            using (var aes = Aes.Create())
            {
                byte[] encryptedBytesWithIv = Convert.FromBase64String(protectedText);
                aes.Key = Convert.FromBase64String(key);

                // Extract the IV from concatenated bytes
                byte[] iv = new byte[aes.IV.Length];
                Array.Copy(encryptedBytesWithIv, iv, iv.Length);
                aes.IV = iv;

                // Extracts protected text from the concatenated bytes
                byte[] protectedBytes = new byte[encryptedBytesWithIv.Length - aes.IV.Length];
                Array.Copy(encryptedBytesWithIv, aes.IV.Length, protectedBytes, 0, protectedBytes.Length);

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                string decryptedText;

                // Decrypt the bytes
                using (var ms = new MemoryStream(protectedBytes))
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (var sr = new StreamReader(cs))
                        {
                            decryptedText = sr.ReadToEnd();
                        }
                    }
                }

                return decryptedText;
            }
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
