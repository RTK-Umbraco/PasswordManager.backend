using PasswordManager.KeyVaults.Domain.Operations;
using PasswordManager.KeyVaults.Infrastructure.BaseRepository;

namespace PasswordManager.KeyVaults.Infrastructure.OperationRepository;
public class OperationEntity : BaseEntity
{
    public string RequestId { get; }
    public Guid SecurityKeyId { get; }
    public string CreatedBy { get; }
    public OperationName OperationName { get; }
    public DateTime? CompletedUtc { get; }
    public OperationStatus Status { get; }
    public string? Data { get; }

    public OperationEntity(Guid id, DateTime createdUtc, DateTime modifiedUtc, string requestId, Guid securitykeyId,
       string createdBy, OperationName operationName, OperationStatus status, DateTime? completedUtc, string? data) :
       base(id, createdUtc, modifiedUtc)
    {
        ModifiedUtc = modifiedUtc;
        RequestId = requestId;
        SecurityKeyId = securitykeyId;
        CreatedBy = createdBy;
        OperationName = operationName;
        Status = status;
        CompletedUtc = completedUtc;
        Data = data;
    }
}
