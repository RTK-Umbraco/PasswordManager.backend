namespace PasswordManager.Users.ApplicationServices.UserPassword.DeleteUserPassword
{
    public class DeleteUserPasswordServiceException : Exception
    {
        public DeleteUserPasswordServiceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
