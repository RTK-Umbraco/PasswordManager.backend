using PasswordManager.Password.Domain.Password;

namespace PasswordManager.Password.Domain.Operations
{
    public static class OperationBuilder
    {
        private static Operation CreateOperation(Guid passwordId, OperationName operationName, string createdBy, Dictionary<string, string>? data)
        {
            return new(
                Guid.NewGuid(),
                Guid.NewGuid().ToString(),
                createdBy,
                passwordId,
                operationName,
                OperationStatus.Queued,
                DateTime.UtcNow,
                DateTime.UtcNow,
                null,
                data);
        }

        public static Operation CreatePassword(PasswordModel passwordModel, string createdBy)
        {
            var data = new Dictionary<string, string>()
            {
                { OperationDataConstants.PasswordCreateUrl, passwordModel.Url },
                { OperationDataConstants.PasswordCreateLabel, passwordModel.Label },
                { OperationDataConstants.PasswordCreateUsername, passwordModel.Username },
                { OperationDataConstants.PasswordCreateKey, passwordModel.Key },
            };

            return CreateOperation(passwordModel.Id, OperationName.CreatePassword, createdBy, data);
        }

        public static Operation UpdatePassword(PasswordModel newPasswordModel, PasswordModel currentPasswordModel, string createdBy)
        {
            var data = new Dictionary<string, string>()
            {
                { OperationDataConstants.CurrentPasswordUrl, currentPasswordModel.Url },
                { OperationDataConstants.CurrentPasswordLabel, currentPasswordModel.Label },
                { OperationDataConstants.CurrentPasswordUsername, currentPasswordModel.Username },
                { OperationDataConstants.CurrentPasswordKey, currentPasswordModel.Key},

                { OperationDataConstants.NewPasswordUrl , newPasswordModel.Url },
                { OperationDataConstants.NewPasswordLabel , newPasswordModel.Label },
                { OperationDataConstants.NewPasswordUsername , newPasswordModel.Username},
                { OperationDataConstants.NewPasswordKey , newPasswordModel.Key},
            };

            return CreateOperation(newPasswordModel.Id, OperationName.UpdatePassword, createdBy, data);
        }
    }
}