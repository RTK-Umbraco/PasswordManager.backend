namespace PasswordManager.Password.ApplicationServices.CreatePassword;
public sealed class CreatePasswordServiceException : Exception
{
    public CreatePasswordServiceException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
