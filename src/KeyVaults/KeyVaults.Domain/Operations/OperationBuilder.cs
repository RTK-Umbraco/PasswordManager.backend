using PasswordManager.KeyVaults.Domain.KeyVaults;

namespace PasswordManager.KeyVaults.Domain.Operations
{
    public static class OperationBuilder
    {
        private static Operation CreateOperation(Guid securityKeyId, OperationType operationType, string createdBy, Dictionary<string, string>? data) 
            => new(Guid.NewGuid(), Guid.NewGuid().ToString(), createdBy, securityKeyId, operationType, OperationStatus.Queued, DateTime.UtcNow, DateTime.UtcNow, null, data);

        public static Operation CreateSecurityKey(SecurityKeyModel securityKeyModel, string createdBy)
        {
            var data = new Dictionary<string, string>
            {
                { OperationDataConstants.CreateSecurityKeySecretKey, securityKeyModel.SecretKey },
                { OperationDataConstants.CreateSecurityKeyObjectId, securityKeyModel.ObjectId.ToString() },
            };

            return CreateOperation(securityKeyModel.Id, OperationType.CreateSecurityKey, createdBy, data);
        }

        public static Operation UpdateSecurityKey(SecurityKeyModel securityKeyModel, string createdBy)
        {
            var data = new Dictionary<string, string>
            {
                { OperationDataConstants.UpdateSecurityKeySecretKey, securityKeyModel.SecretKey },
                { OperationDataConstants.UpdateSecurityKeyObjectId, securityKeyModel.ObjectId.ToString() },
            };

            return CreateOperation(securityKeyModel.Id, OperationType.UpdateSecurityKey, createdBy, data);
        }

        public static Operation DeleteSecurityKey(SecurityKeyModel securityKeyModel, string createdBy)
        {
            var data = new Dictionary<string, string>
            {
                { OperationDataConstants.DeleteSecurityKeySecretKey, securityKeyModel.SecretKey },
                { OperationDataConstants.DeleteSecurityKeyObjectId, securityKeyModel.ObjectId.ToString() },
            };

            return CreateOperation(securityKeyModel.Id, OperationType.DeleteSecurityKey, createdBy, data);
        }
    }
}
