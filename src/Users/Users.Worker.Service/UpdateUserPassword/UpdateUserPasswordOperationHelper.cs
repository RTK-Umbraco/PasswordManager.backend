using PasswordManager.Users.Domain.Operations;
using PasswordManager.Users.Domain.User;

namespace Users.Worker.Service.UpdateUserPassword
{
    public class UpdateUserPasswordOperationHelper
    {
        internal static UserPasswordModel Map(Guid userId, Operation operation)
        {
            return UserPasswordModel.UpdateUserPassword(userId, GetPasswordId(operation), GetPasswordUrl(operation), GetPasswordLabel(operation), GetPasswordUsername(operation), GetPasswordKey(operation));
        }

        private static string GetUserPasswordOperationData(Operation operation, string operationDataConstant)
        {
            if (operation.Data is null || operation.Data.TryGetValue(operationDataConstant, out var getPasswordOperationData) is false)
                throw new InvalidOperationException($"Could not find user password {operationDataConstant} in operation with request id {operation.RequestId} when creating user password");

            return getPasswordOperationData;
        }

        private static Guid GetPasswordId(Operation operation)
        {
            if (operation.Data is null || operation.Data.TryGetValue(OperationDataConstants.UserPasswordId, out var getPasswordId) is false)
                throw new InvalidOperationException($"Could not find user password id in operation with request id {operation.RequestId} when updating user password");

            return Guid.Parse(getPasswordId);
        }

        private static string GetPasswordUrl(Operation operation)
        {
            return GetUserPasswordOperationData(operation, OperationDataConstants.NewUserPasswordUrl);
        }

        private static string GetPasswordLabel(Operation operation)
        {
            return GetUserPasswordOperationData(operation, OperationDataConstants.NewUserPasswordFriendlyName);
        }

        private static string GetPasswordUsername(Operation operation)
        {
            return GetUserPasswordOperationData(operation, OperationDataConstants.NewUserPasswordUsername);
        }

        private static string GetPasswordKey(Operation operation)
        {
            return GetUserPasswordOperationData(operation, OperationDataConstants.NewUserPasswordPassword);
        }
    }
}
