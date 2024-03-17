using KeyVaults.Messages.CreateSecurityKey;
using PasswordManager.KeyVaults.ApplicationServices.CreateSecurityKey;
using PasswordManager.KeyVaults.ApplicationServices.Operations;
using PasswordManager.KeyVaults.Domain.Operations;
using Rebus.Handlers;

namespace KeyVaults.Worker.Service.CreateSecurityKey
{
    public sealed class CreateSecurityKeyCommandHandler : IHandleMessages<CreateSecurityKeyCommand>
    {
        private readonly ICreateSecurityKeyService _createSecurityKeyService;
        private readonly IOperationService _operationService;
        private readonly ILogger<CreateSecurityKeyCommandHandler> _logger;

        public CreateSecurityKeyCommandHandler(ICreateSecurityKeyService createSecurityKeyService, IOperationService operationService, ILogger<CreateSecurityKeyCommandHandler> logger)
        {
            _createSecurityKeyService = createSecurityKeyService;
            _operationService = operationService;
            _logger = logger;
        }

        public async Task Handle(CreateSecurityKeyCommand message)
        {
            _logger.LogInformation($"Handling creation of security key command: {message.RequestId}");

            var operation = await _operationService.GetOperationByRequestId(message.RequestId);

            if (operation == null)
            {
                _logger.LogError($"Operation not found: {message.RequestId}");
                return;
            }

            await _operationService.UpdateOperationStatus(message.RequestId, OperationStatus.Processing);

            var securityKeyModel = CreateSecurityKeyOperationHelper.Map(operation.SecurityKeyId, operation);

            if (securityKeyModel == null)
            {
                _logger.LogError($"Security key model not found: {message.RequestId}");
                return;
            }

            await _createSecurityKeyService.CreateSecurityKey(securityKeyModel);

            await _operationService.UpdateOperationStatus(message.RequestId, OperationStatus.Completed);

            _logger.LogInformation($"Security key created: {message.RequestId}");

            OperationResult.Completed(operation);
        }
    }
}
