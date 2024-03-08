namespace Password.Messages.UpdatePassword;
public class UpdatePasswordFailedEvent : AbstractPasswordFailedEvent
{
    public Guid PasswordId { get; }
    public string RequestId { get; }

    public UpdatePasswordFailedEvent(Guid passwordId, string requestId, string errorMessage) : base(errorMessage)
    {
        PasswordId = passwordId;
        RequestId = requestId;
    }
}
