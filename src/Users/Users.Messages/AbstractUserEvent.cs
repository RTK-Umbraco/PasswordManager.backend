namespace Users.Messages;
public abstract class AbstractUserEvent
{
    public Guid UserId { get; }
    public string RequestId { get; }
    public AbstractUserEvent(Guid userId, string requestId)
    {
        UserId = userId;
        RequestId = requestId;
    }
}
