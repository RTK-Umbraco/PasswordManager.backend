namespace PasswordManager.KeyVaults.ApplicationServices.Protection
{
    public interface IProtectionService
    {
        string Protect(string plainText, string key);
        string Unprotect(string protectedText, string key);
        string GenerateSecretKey(int length);
    }
}
