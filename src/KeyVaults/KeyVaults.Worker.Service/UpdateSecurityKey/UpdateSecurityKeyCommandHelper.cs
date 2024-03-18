using KeyVaults.Messages.UpdateSecurityKey;
using PasswordManager.KeyVaults.ApplicationServices.Operations;
using PasswordManager.KeyVaults.ApplicationServices.UpdateSecurityKey;
using PasswordManager.KeyVaults.Domain.Operations;
using Rebus.Handlers;

namespace KeyVaults.Worker.Service.UpdateSecurityKey
{
    public sealed class UpdateSecurityKeyCommandHelper : IHandleMessages<UpdateSecurityKeyCommand>
    {
        private readonly IUpdateSecurityKeyService _updateSecurityKeyService;
        private readonly IOperationService _operationService;
        private readonly ILogger<UpdateSecurityKeyCommandHelper> _logger;

        public UpdateSecurityKeyCommandHelper(IUpdateSecurityKeyService updateSecurityKeyService, IOperationService operationService, ILogger<UpdateSecurityKeyCommandHelper> logger)
        {
            _updateSecurityKeyService = updateSecurityKeyService;
            _operationService = operationService;
            _logger = logger;
        }

        public async Task Handle(UpdateSecurityKeyCommand message)
        {
            _logger.LogInformation($"Handling update of security key command: {message.RequestId}");

            var operation = await _operationService.GetOperationByRequestId(message.RequestId);

            if (operation == null)
            {
                _logger.LogError($"Operation not found: {message.RequestId}");
                return;
            }

            await _operationService.UpdateOperationStatus(message.RequestId, OperationStatus.Processing);

            var securityKeyModel = UpdateSecurityKeyOperationHelper.Map(operation.SecurityKeyId, operation);

            if (securityKeyModel == null)
            {
                _logger.LogError($"Security key model not found: {message.RequestId}");
                return;
            }

            await _updateSecurityKeyService.UpdateSecurityKey(securityKeyModel);

            await _operationService.UpdateOperationStatus(message.RequestId, OperationStatus.Completed);

            _logger.LogInformation($"Security key updated: {message.RequestId}");

            OperationResult.Completed(operation);
        }
    }
}
