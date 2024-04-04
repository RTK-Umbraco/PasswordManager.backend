namespace Users.Messages.DeleteUserPassword
{
    public class DeleteUserPasswordCommand : AbstractRequestAcceptedCommand
    {
        public DeleteUserPasswordCommand(string requestId) : base(requestId)
        {
        }
    }
}
