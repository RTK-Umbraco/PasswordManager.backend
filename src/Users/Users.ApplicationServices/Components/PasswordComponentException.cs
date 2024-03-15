namespace PasswordManager.Users.ApplicationServices.Components;
public class PasswordComponentException : Exception
{
    public PasswordComponentException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
