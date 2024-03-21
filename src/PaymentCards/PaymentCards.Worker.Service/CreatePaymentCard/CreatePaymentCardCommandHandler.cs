using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using PasswordManager.PaymentCards.ApplicationServices.Operations;
using PasswordManager.PaymentCards.ApplicationServices.PaymentCard.CreatePaymentCard;
using PasswordManager.PaymentCards.Domain.Operations;
using PaymentCards.Messages.CreatePaymentCard;
using Rebus.Handlers;

namespace PaymentCards.Worker.Service.CreatePaymentCard
{
    public sealed class CreatePaymentCardCommandHandler : IHandleMessages<CreatePaymentCardCommand>
    {
        private readonly ICreatePaymentCardService _createPaymentCardService;
        private readonly IOperationService _operationService;
        private readonly ILogger<CreatePaymentCardCommandHandler> _logger;

        public CreatePaymentCardCommandHandler(ICreatePaymentCardService createPaymentCardService, IOperationService operationService, ILogger<CreatePaymentCardCommandHandler> logger)
        {
            _createPaymentCardService = createPaymentCardService;
            _operationService = operationService;
            _logger = logger;
        }

        public async Task Handle(CreatePaymentCardCommand message)
        {
            _logger.LogInformation($"Handling creation of PaymentCard command: {message.RequestId}");

            var operation = await _operationService.GetOperationByRequestId(message.RequestId);

            if (operation == null)
            {
                _logger.LogError($"Operation not found: {message.RequestId}");
                return;
            }

            await _operationService.UpdateOperationStatus(message.RequestId, OperationStatus.Processing);

            var paymentCardModel = CreatePaymentCardOperationHelper.Map(operation.PaymentCardId, operation);

            if (paymentCardModel == null)
            {
                _logger.LogError($"PaymentCard model not found: {operation.PaymentCardId}");
                return;
            }

            await _createPaymentCardService.CreatePaymentCard(paymentCardModel);

            await _operationService.UpdateOperationStatus(message.RequestId, OperationStatus.Completed);

            _logger.LogInformation($"PaymentCard created: {operation.PaymentCardId}");

            OperationResult.Completed(operation);
        }
    }
}
