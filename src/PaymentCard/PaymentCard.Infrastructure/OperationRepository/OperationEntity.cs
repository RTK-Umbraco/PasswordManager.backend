using PasswordManager.PaymentCard.Domain.Operations;
using PasswordManager.PaymentCard.Infrastructure.BaseRepository;

namespace PasswordManager.PaymentCard.Infrastructure.OperationRepository;
public class OperationEntity : BaseEntity
{
    public string RequestId { get; }
    public Guid PaymentCardId { get; }
    public string CreatedBy { get; }
    public OperationName OperationName { get; }
    public DateTime? CompletedUtc { get; }
    public OperationStatus Status { get; }
    public string? Data { get; }

    public OperationEntity(Guid id, DateTime createdUtc, DateTime modifiedUtc, string requestId, Guid paymentcardId,
       string createdBy, OperationName operationName, OperationStatus status, DateTime? completedUtc, string? data) :
       base(id, createdUtc, modifiedUtc)
    {
        ModifiedUtc = modifiedUtc;
        RequestId = requestId;
        PaymentCardId = paymentcardId;
        CreatedBy = createdBy;
        OperationName = operationName;
        Status = status;
        CompletedUtc = completedUtc;
        Data = data;
    }
}
