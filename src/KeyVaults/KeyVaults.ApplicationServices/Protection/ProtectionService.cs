using System.Security.Cryptography;

namespace PasswordManager.KeyVaults.ApplicationServices.Protection
{
    public class ProtectionService : IProtectionService
    {
        public string Protect(string item, string key)
        {
            using (var aes = Aes.Create())
            {
                byte[] keyBytes;

                // Converts from base64 string to byte array
                try
                {
                    keyBytes = Convert.FromBase64String(key);
                }
                catch (Exception ex)
                {
                    throw new ProtectionServiceException("ERROR: Could not convert key from Base64");
                }

                if (aes.ValidKeySize(keyBytes.Length * 8) == false)
                {
                    throw new ProtectionServiceException($"ERROR: Key size does not meet requirements! Key size: '{keyBytes.Length * 8} bit', Accepted key sizes: 128 bit, 192 bit, 256 bit");
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
                            sw.Write(item);
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

        public string Unprotect(string protectedItem, string key)
        {
            using (var aes = Aes.Create())
            {
                // Extract the protected item from the base64 string
                byte[] encryptedBytesWithIv;
                try
                {
                    encryptedBytesWithIv = Convert.FromBase64String(protectedItem);
                } catch (Exception ex)
                {
                    throw new ProtectionServiceException("ERROR: Could not convert protected item from Base64");
                }

                // Extract the key from the base64 string
                byte[] keyBytes;
                try
                {
                    keyBytes = Convert.FromBase64String(key);
                } catch (Exception ex)
                {
                    throw new ProtectionServiceException("ERROR: Could not convert key from Base64");
                }

                // Set the key
                aes.Key = keyBytes;

                // Extract the IV from concatenated bytes
                byte[] iv = new byte[aes.IV.Length];
                Array.Copy(encryptedBytesWithIv, iv, iv.Length);
                aes.IV = iv;

                // Extracts protected item from the concatenated bytes
                byte[] protectedBytes = new byte[encryptedBytesWithIv.Length - aes.IV.Length];
                Array.Copy(encryptedBytesWithIv, aes.IV.Length, protectedBytes, 0, protectedBytes.Length);

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                string decryptedItem;

                // Decrypt the bytes
                using (var ms = new MemoryStream(protectedBytes))
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (var sr = new StreamReader(cs))
                        {
                            decryptedItem = sr.ReadToEnd();
                        }
                    }
                }

                return decryptedItem;
            }
        }
    }
}
