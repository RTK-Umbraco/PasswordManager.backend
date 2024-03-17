using KeyVaults.Messages.DeleteSecurityKey;
using PasswordManager.KeyVaults.ApplicationServices.DeleteSecurityKey;
using PasswordManager.KeyVaults.ApplicationServices.Operations;
using PasswordManager.KeyVaults.Domain.Operations;

using Rebus.Handlers;

namespace KeyVaults.Worker.Service.DeleteSecurityKey
{
    public sealed class DeleteSecurityKeyCommandHandler : IHandleMessages<DeleteSecurityKeyCommand>
    {
        private readonly IDeleteSecurityKeyService _deleteSecurityKeyService;
        private readonly IOperationService _operationService;
        private readonly ILogger<DeleteSecurityKeyCommandHandler> _logger;

        public DeleteSecurityKeyCommandHandler(IDeleteSecurityKeyService deleteSecurityKeyService, IOperationService operationService, ILogger<DeleteSecurityKeyCommandHandler> logger)
        {
            _deleteSecurityKeyService = deleteSecurityKeyService;
            _operationService = operationService;
            _logger = logger;
        }

        public async Task Handle(DeleteSecurityKeyCommand message)
        {
            _logger.LogInformation($"Handling deletion of security key command: {message.RequestId}");

            var operation = await _operationService.GetOperationByRequestId(message.RequestId);

            if (operation == null)
            {
                _logger.LogError($"Operation not found: {message.RequestId}");
                return;
            }

            await _operationService.UpdateOperationStatus(message.RequestId, OperationStatus.Processing);

            var securityKeyModel = DeleteSecurityKeyOperationHelper.Map(operation.SecurityKeyId, operation);

            if (securityKeyModel == null)
            {
                _logger.LogError($"Security key model not found: {message.RequestId}");
                return;
            }

            await _deleteSecurityKeyService.DeleteSecurityKey(securityKeyModel.Id);

            await _operationService.UpdateOperationStatus(message.RequestId, OperationStatus.Completed);

            _logger.LogInformation($"Security key deleted: {message.RequestId}");

            OperationResult.Completed(operation);
        }
    }
}
