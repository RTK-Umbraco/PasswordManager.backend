namespace PasswordManager.Users.ApplicationServices.GetUserPasswords;
internal class GetUserPasswordsServiceException : Exception
{
    public GetUserPasswordsServiceException(string? message) : base(message)
    {
    }

    public GetUserPasswordsServiceException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
