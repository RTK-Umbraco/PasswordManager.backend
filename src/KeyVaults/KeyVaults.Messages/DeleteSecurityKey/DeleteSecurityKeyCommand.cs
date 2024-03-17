namespace KeyVaults.Messages.DeleteSecurityKey
{
    public sealed class DeleteSecurityKeyCommand : AbstractRequestAcceptedCommand
    {
        public DeleteSecurityKeyCommand(string requestId) : base(requestId)
        {
        }
    }
}
