using PasswordManager.PaymentCards.Domain.Operations;
using PasswordManager.PaymentCards.Domain.PaymentCards;

namespace PaymentCards.Worker.Service.UpdatePaymentCard
{
    internal class UpdatePaymentCardOperationHelper
    {
        internal static PaymentCardModel Map(Guid paymentCardId, Operation operation)
            => new PaymentCardModel(
                paymentCardId,
                GetPaymentCardCardNumber(operation),
                GetPaymentCardCardHolderName(operation),
                GetPaymentCardExpirationDate(operation)
                );

        private static string GetPaymentCardOperationData(Operation operation, string operationDataConstant)
        {
            if (operation.Data is null || operation.Data.TryGetValue(operationDataConstant, out var getPaymentCardOperationData) is false)
                throw new InvalidOperationException($"Could not find PaymentCard: {operationDataConstant}, in operation with request id: {operation.RequestId}");

            return getPaymentCardOperationData;
        }

        private static string GetPaymentCardCardNumber(Operation operation)
            => GetPaymentCardOperationData(operation, OperationDataConstants.NewPaymentCardCardNumber);

        private static string GetPaymentCardCardHolderName(Operation operation)
            => GetPaymentCardOperationData(operation, OperationDataConstants.NewPaymentCardCardholderName);

        private static string GetPaymentCardExpirationDate(Operation operation)
            => GetPaymentCardOperationData(operation, OperationDataConstants.NewPaymentCardExpirationDate);
    }
}
