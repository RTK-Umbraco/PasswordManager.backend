namespace Users.Messages.CreateUserPassword;
public class CreateUserPasswordEvent : AbstractUserEvent
{
    public CreateUserPasswordEvent(Guid userId, string requestId) : base(userId, requestId)
    {
    }
}
