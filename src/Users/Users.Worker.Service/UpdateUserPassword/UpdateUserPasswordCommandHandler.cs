using PasswordManager.Users.ApplicationServices.Operations;
using PasswordManager.Users.ApplicationServices.UserPassword.UpdateUserPassword;
using PasswordManager.Users.Domain.Operations;
using Rebus.Bus;
using Rebus.Handlers;
using Users.Messages.UpdateUserPassword;
using Users.Worker.Service.CreateUserPassword;

namespace Users.Worker.Service.UpdateUserPassword
{
    public class UpdateUserPasswordCommandHandler : IHandleMessages<UpdateUserPasswordCommand>
    {
        private readonly IUpdateUserPasswordService _updateUserPasswordService;
        private readonly IOperationService _operationService;
        private readonly ILogger<UpdateUserPasswordCommandHandler> _logger;
        private readonly IBus _bus;

        public UpdateUserPasswordCommandHandler(IUpdateUserPasswordService updateUserPasswordService, IOperationService operationService, ILogger<UpdateUserPasswordCommandHandler> logger, IBus bus)
        {
            _updateUserPasswordService = updateUserPasswordService;
            _operationService = operationService;
            _logger = logger;
            _bus = bus;
        }

        public async Task Handle(UpdateUserPasswordCommand message)
        {
            _logger.LogInformation("Handling update user password command {requestId}", message.RequestId);

            var operation = await _operationService.GetOperationByRequestId(message.RequestId);

            if (operation == null)
            {
                _logger.LogWarning("Operation not found for requestId {requestId}", message.RequestId);
                return;
            }

            await _operationService.UpdateOperationStatus(operation.RequestId, OperationStatus.Processing);
            var updateUserPasswordModel = CreateUserPasswordOperationHelper.Map(operation.UserId, operation);

            try
            {
                await _updateUserPasswordService.UpdateUserPassword(updateUserPasswordModel, operation.CreatedBy);
                await _operationService.UpdateOperationStatus(operation.RequestId, OperationStatus.Completed);
            }
            catch (UpdateUserPasswordServiceException exception)
            {
                _logger.LogError(exception, "Error updating user password with request id: {requestId}", message.RequestId);
                await _operationService.UpdateOperationStatus(operation.RequestId, OperationStatus.Failed);
                return;
            }

        }
    }
}
