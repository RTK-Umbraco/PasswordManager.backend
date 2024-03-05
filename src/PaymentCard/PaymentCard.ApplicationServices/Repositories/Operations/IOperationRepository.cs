using PasswordManager.PaymentCard.Domain.Operations;

namespace PasswordManager.PaymentCard.ApplicationServices.Repositories.Operations;
public interface IOperationRepository : IBaseRepository<Operation>
{
    Task<Operation?> GetByRequestId(string requestId);
    Task<ICollection<Operation>> GetPaymentCardOperations(Guid paymentcardId);
}