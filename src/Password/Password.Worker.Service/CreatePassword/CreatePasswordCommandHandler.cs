using Rebus.Handlers;
using Password.Messages.CreatePassword;
using PasswordManager.Password.ApplicationServices.AddPassword;
using PasswordManager.Password.ApplicationServices.Operations;
using PasswordManager.Password.Domain.Operations;

namespace Password.Worker.Service.CreatePassword
{
    public class CreatePasswordCommandHandler : IHandleMessages<CreatePasswordCommand>
    {
        private readonly ICreatePasswordService _createPasswordService;
        private readonly IOperationService _operationService;
        private readonly ILogger<CreatePasswordCommandHandler> _logger;

        public CreatePasswordCommandHandler(ICreatePasswordService createPasswordService, IOperationService operationService, ILogger<CreatePasswordCommandHandler> logger)
        {
            _createPasswordService = createPasswordService;
            _operationService = operationService;
            _logger = logger;
        }

        public async Task Handle(CreatePasswordCommand message)
        {
            _logger.LogInformation($"Handling create password command: {message.RequestId}");

            var operation = await _operationService.GetOperationByRequestId(message.RequestId);

            if (operation == null)
            {
                _logger.LogWarning($"Operation not found: {message.RequestId}");
                return;
            }

            await _operationService.UpdateOperationStatus(message.RequestId, OperationStatus.Processing);
            
            var createPasswordModel = CreatePasswordOperationHelper.Map(operation.PasswordId, operation);
            
            await _createPasswordService.CreatePassword(createPasswordModel);
            
            await _operationService.UpdateOperationStatus(message.RequestId, OperationStatus.Completed);

            OperationResult.Completed(operation);
        }
    }
}
