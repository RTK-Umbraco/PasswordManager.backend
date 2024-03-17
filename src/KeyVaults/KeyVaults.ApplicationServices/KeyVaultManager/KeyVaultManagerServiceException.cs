namespace PasswordManager.KeyVaults.ApplicationServices.KeyVaultManager
{
    public class KeyVaultManagerServiceException : Exception
    {
        public KeyVaultManagerServiceException(string message) : base(message)
        {
        }

        public KeyVaultManagerServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
