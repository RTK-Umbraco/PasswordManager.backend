namespace PasswordManager.Password.Domain.VaultObject
{
    public class VaultObjectModel : BaseModel
    {
        public Guid UserId { get; private set; }
        public Guid SecurityKeyId { get; private set; }
        public string EncryptedData { get; private set; }
        public VaultObjectTypeEnum ObjectType { get; }

        public VaultObjectModel(Guid id, string encryptedData, VaultObjectTypeEnum objectType) : base(id)
        {
            EncryptedData = encryptedData;
            ObjectType = objectType;
        }

        public VaultObjectModel(Guid id, DateTime createdUtc, DateTime modifiedUtc, bool deleted, string encryptedData, VaultObjectTypeEnum objectType) : base(id, createdUtc, modifiedUtc, deleted)
        {
            EncryptedData = encryptedData;
            ObjectType = objectType;
        }

        public static VaultObjectModel Create(Guid userId, Guid securityKeyId, string encryptedData, VaultObjectTypeEnum objectType)
        {
            return new VaultObjectModel(Guid.NewGuid(), encryptedData, objectType)
            {
                UserId = userId,
                SecurityKeyId = securityKeyId
            };
        }

        public static VaultObjectModel UpdateVaultObject(Guid id, string encryptedData, VaultObjectTypeEnum objectType)
        {
            return new VaultObjectModel(id, encryptedData, objectType);
        }
    }
}
