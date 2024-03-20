using PasswordManager.Users.Domain.Operations;
using PasswordManager.Users.Domain.User;

namespace Users.Worker.Service.CreateUserPassword;

public class CreateUserPasswordOperationHelper
{
    internal static UserPasswordModel Map(Guid userId, Operation operation)
    {
        return new UserPasswordModel(userId, GetPasswordUrl(operation), GetPasswordLabel(operation), GetPasswordUsername(operation), GetPasswordKey(operation));
    }

    private static string GetPasswordOperationData(Operation operation, string operationDataConstant)
    {
        if (operation.Data is null || operation.Data.TryGetValue(operationDataConstant, out var getPasswordOperationData) is false)
            throw new InvalidOperationException($"Could not find password {operationDataConstant} in operation with request id {operation.RequestId} when creating password");

        return getPasswordOperationData;
    }

    private static string GetPasswordUrl(Operation operation)
    {
        return GetPasswordOperationData(operation, OperationDataConstants.CreateUserPasswordUrl);
    }

    private static string GetPasswordLabel(Operation operation)
    {
        return GetPasswordOperationData(operation, OperationDataConstants.CreateUserPasswordFriendlyName);
    }

    private static string GetPasswordUsername(Operation operation)
    {
        return GetPasswordOperationData(operation, OperationDataConstants.CreateUserPasswordUsername);
    }

    private static string GetPasswordKey(Operation operation)
    {
        return GetPasswordOperationData(operation, OperationDataConstants.CreateUserPasswordPassword);
    }
}
