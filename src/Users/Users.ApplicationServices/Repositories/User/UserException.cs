namespace PasswordManager.Users.ApplicationServices.Repositories.User;
internal class UserException : Exception
{
    public UserException(string? message) : base(message) { }
}
