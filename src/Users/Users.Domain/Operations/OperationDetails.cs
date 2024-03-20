namespace PasswordManager.Users.Domain.Operations
{
    public class OperationDetails
    {
        public OperationDetails(string createdBy, string? correlationRequestId = null)
        {
            CreatedBy = createdBy;
            CorrelationRequestId = correlationRequestId;
        }

        public string? CorrelationRequestId { get; }
        public string CreatedBy { get; }
    }
}
