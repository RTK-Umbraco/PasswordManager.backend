using PasswordManager.KeyVaults.Domain.Operations;
using PasswordManager.KeyVaults.Infrastructure.BaseRepository;

namespace PasswordManager.KeyVaults.Infrastructure.OperationRepository;
public class OperationEntity : BaseEntity
{
    public string RequestId { get; private set; }
    public Guid SecurityKeyId { get; private set; }
    public string CreatedBy { get; private set; }
    public OperationType OperationName { get; private set; }
    public DateTime? CompletedUtc { get; private set; }
    public OperationStatus Status { get; private set; }
    public string? Data { get; private set; }

    public OperationEntity(Guid id, 
        DateTime createdUtc, 
        DateTime modifiedUtc, 
        string requestId, 
        Guid securityKeyId, 
        string createdBy, 
        OperationType operationName, 
        OperationStatus status, 
        DateTime? completedUtc, 
        string? data,
        bool deleted) : base(id, createdUtc, modifiedUtc, deleted)
    {
        ModifiedUtc = modifiedUtc;
        RequestId = requestId;
        SecurityKeyId = securityKeyId;
        CreatedBy = createdBy;
        OperationName = operationName;
        Status = status;
        CompletedUtc = completedUtc;
        Data = data;
    }
}
