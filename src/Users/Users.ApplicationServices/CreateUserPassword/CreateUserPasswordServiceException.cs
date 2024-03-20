namespace PasswordManager.Users.ApplicationServices.CreateUserPassword;
public class CreateUserPasswordServiceException : Exception
{
    public CreateUserPasswordServiceException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
