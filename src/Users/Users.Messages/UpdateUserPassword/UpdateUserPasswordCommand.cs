namespace Users.Messages.UpdateUserPassword
{
    public class UpdateUserPasswordCommand : AbstractRequestAcceptedCommand
    {
        public UpdateUserPasswordCommand(string requestId) : base(requestId)
        {
        }
    }
}
