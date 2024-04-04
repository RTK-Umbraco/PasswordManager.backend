using PasswordManager.Users.Domain.Operations;
using PasswordManager.Users.Domain.User;

namespace PasswordManager.User.Domain.Operations;

public static class OperationBuilder
{
    private static Operation CreateOperation(Guid userId, OperationName operationName, string createdBy, Dictionary<string, string>? data)
    => new(Guid.NewGuid(), Guid.NewGuid().ToString(), createdBy, userId, operationName, OperationStatus.Queued, DateTime.UtcNow, DateTime.UtcNow, null, data);

    public static Operation CreateUserPassword(UserPasswordModel passwordModel, string createdBy)
    {
        var data = new Dictionary<string, string>()
        {
            { OperationDataConstants.CreateUserPasswordUrl, passwordModel.Url },
            { OperationDataConstants.CreateUserPasswordFriendlyName, passwordModel.FriendlyName },
            { OperationDataConstants.CreateUserPasswordUsername, passwordModel.Username },
            { OperationDataConstants.CreateUserPasswordPassword, passwordModel.Password },
        };

        return CreateOperation(passwordModel.UserId, OperationName.CreateUserPassword, createdBy, data);
    }

    public static Operation UpdateUserPassword(UserPasswordModel newPassword, string createdBy)
    {
        var data = new Dictionary<string, string>()
        {
            { OperationDataConstants.UserPasswordId, newPassword.PasswordId.ToString() },
            { OperationDataConstants.NewUserPasswordUrl, newPassword.Url },
            { OperationDataConstants.NewUserPasswordFriendlyName, newPassword.FriendlyName },
            { OperationDataConstants.NewUserPasswordUsername, newPassword.Username },
            { OperationDataConstants.NewUserPasswordPassword, newPassword.Password },
        };

        return CreateOperation(newPassword.UserId, OperationName.UpdateUserPassword, createdBy, data);
    }

    public static Operation DeleteUserPassword(Guid userId, string createdBy)
    {
        var data = new Dictionary<string, string>()
        {
            { OperationDataConstants.UserPasswordId, userId.ToString() }
        };

        return CreateOperation(userId, OperationName.DeleteUserPassword, createdBy, data);
    }
}