using PasswordManager.KeyVaults.Domain.KeyVaults;
using PasswordManager.KeyVaults.Domain.Operations;

namespace KeyVaults.Worker.Service.UpdateSecurityKey
{
    public class UpdateSecurityKeyOperationHelper
    {
        internal static SecurityKeyModel Map(Guid securityKeyId, Operation operation)
            => new SecurityKeyModel(securityKeyId, GetSecurityKeySecretKey(operation), GetSecurityKeyObjectId(operation));

        private static string GetSecurityKeyOperationData(Operation operation, string operationDataConstant)
        {
            if (operation.Data is null || operation.Data.TryGetValue(operationDataConstant, out var getSecurityKeyOperationData) is false)
                throw new InvalidOperationException($"Could not find SecurityKey: {operationDataConstant}, in operation with request id: {operation.RequestId}");

            return getSecurityKeyOperationData;
        }

        private static string GetSecurityKeySecretKey(Operation operation)
            => GetSecurityKeyOperationData(operation, OperationDataConstants.UpdateSecurityKeySecretKey);

        private static Guid GetSecurityKeyObjectId(Operation operation)
        {
            var objectIdStr = GetSecurityKeyOperationData(operation, OperationDataConstants.UpdateSecurityKeyObjectId);
            if (!Guid.TryParse(objectIdStr, out Guid objectId))
                throw new InvalidOperationException($"Invalid or missing SecurityKey ObjectId in operation with request id: {operation.RequestId}");
            return objectId;
        }
    }
}
