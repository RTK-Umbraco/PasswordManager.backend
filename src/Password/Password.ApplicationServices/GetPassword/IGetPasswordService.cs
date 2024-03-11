namespace PasswordManager.Password.ApplicationServices.GetPassword
{
    public interface IGetPasswordService
    {
        Task<PasswordModel?> GetPassword(Guid passwordId);
        Task<IEnumerable<PasswordModel>> GetPasswords();
    }
}
