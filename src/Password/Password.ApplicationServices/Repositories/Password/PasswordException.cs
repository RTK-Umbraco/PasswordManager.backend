namespace PasswordManager.Password.ApplicationServices.Repositories.Password;
internal class PasswordException : Exception
{
    public PasswordException(string? message) : base(message) { }
}
