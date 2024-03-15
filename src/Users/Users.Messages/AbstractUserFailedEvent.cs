namespace Users.Messages;
public abstract class AbstractUserFailedEvent : AbstractUserEvent
{
    public string Message { get; }

    public AbstractUserFailedEvent(Guid userId, string requestId, string message) : base(userId, requestId)
    {
        Message = message;
    }
}
