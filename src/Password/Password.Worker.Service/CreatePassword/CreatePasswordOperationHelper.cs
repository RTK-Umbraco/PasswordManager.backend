using PasswordManager.Password.Domain.Operations;
using PasswordManager.Password.Domain.Password;

namespace Password.Worker.Service.CreatePassword
{
    internal class CreatePasswordOperationHelper
    {
        internal static PasswordModel Map(Guid passwordId, Operation operation)
        {
            return new PasswordModel(passwordId, GetPasswordUrl(operation), GetPasswordLabel(operation), GetPasswordUsername(operation), GetPasswordKey(operation));
        }

        private static string GetCreatePassword(Operation operation, string operationDataConstant)
        {
            if (operation.Data is null || operation.Data.TryGetValue(operationDataConstant, out var createPassword) is false)
                throw new InvalidOperationException($"Could not find password {operationDataConstant} in operation with request id {operation.RequestId} when creating password");

            return createPassword;
        }

        private static string GetPasswordUrl(Operation operation)
        {
            return GetCreatePassword(operation, OperationDataConstants.PasswordCreateUrl);
        }

        private static string GetPasswordLabel(Operation operation)
        {
            return GetCreatePassword(operation, OperationDataConstants.PasswordCreateLabel);
        }

        private static string GetPasswordUsername(Operation operation)
        {
            return GetCreatePassword(operation, OperationDataConstants.PasswordCreateUsername);
        }

        private static string GetPasswordKey(Operation operation)
        {
            return GetCreatePassword(operation, OperationDataConstants.PasswordCreateKey);
        }
    }
}
