namespace PasswordManager.Users.ApplicationServices.UserPassword.CreateUserPassword;
public class CreateUserPasswordServiceException : Exception
{
    public CreateUserPasswordServiceException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
