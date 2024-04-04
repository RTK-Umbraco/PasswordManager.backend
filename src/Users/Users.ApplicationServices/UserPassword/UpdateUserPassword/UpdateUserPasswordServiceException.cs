namespace PasswordManager.Users.ApplicationServices.UserPassword.UpdateUserPassword
{
    public class UpdateUserPasswordServiceException : Exception
    {
        public UpdateUserPasswordServiceException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
