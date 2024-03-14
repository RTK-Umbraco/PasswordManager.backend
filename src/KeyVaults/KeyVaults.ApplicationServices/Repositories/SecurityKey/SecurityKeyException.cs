namespace PasswordManager.KeyVaults.ApplicationServices.Repositories.SecurityKey;
internal class SecurityKeyException : Exception
{
    public SecurityKeyException(string? message) : base(message) { }
}
