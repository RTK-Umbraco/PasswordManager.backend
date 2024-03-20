namespace Users.Messages.CreateUserPassword;
public class CreateUserPasswordCommand : AbstractRequestAcceptedCommand
{
    public CreateUserPasswordCommand(string requestId) : base(requestId)
    {
    }
}
