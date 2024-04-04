using PasswordManager.Users.Domain.Operations;

namespace Users.Worker.Service.DeleteUserPassword
{
    public class DeleteUserPasswordOperationHelper
    {
        internal static Guid Map(Operation operation)
        {
            return GetPasswordId(operation);
        }

        private static string GetUserPasswordOperationData(Operation operation, string operationDataConstant)
        {
            if (operation.Data is null || operation.Data.TryGetValue(operationDataConstant, out var getPasswordOperationData) is false)
                throw new InvalidOperationException($"Could not find user password {operationDataConstant} in operation with request id {operation.RequestId} when creating user password");

            return getPasswordOperationData;
        }

        private static Guid GetPasswordId(Operation operation)
        {
            var getPasswordId = GetUserPasswordOperationData(operation, OperationDataConstants.UserPasswordId);
            return Guid.Parse(getPasswordId);
        }
    }
}
