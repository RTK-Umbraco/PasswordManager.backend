namespace Users.Messages.CreateUserPassword;
public class CreateUserPasswordFailedEvent : AbstractUserFailedEvent
{
    public CreateUserPasswordFailedEvent(Guid userId, string requestId, string message) : base(userId, requestId, message)
    {
    }
}
