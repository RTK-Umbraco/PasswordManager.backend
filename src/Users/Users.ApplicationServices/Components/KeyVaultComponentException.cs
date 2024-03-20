namespace PasswordManager.Users.ApplicationServices.Components;
public class KeyVaultComponentException : Exception
{
    public KeyVaultComponentException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
