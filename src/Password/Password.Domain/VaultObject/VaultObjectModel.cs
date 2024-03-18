namespace PasswordManager.Password.Domain.VaultObject
{
    public class VaultObjectModel : BaseModel
    {
        public Guid UserId { get; private set; }
        public string EncryptedData { get; private set; }
        public VaultObjectTypeEnum ObjectType { get; }

        public VaultObjectModel(Guid id, string encryptedData, VaultObjectTypeEnum objectType) : base(id)
        {
            EncryptedData = encryptedData;
            ObjectType = objectType;
        }
    }
}
