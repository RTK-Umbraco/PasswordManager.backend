namespace KeyVaults.Messages.UpdateSecurityKey
{
    public sealed class UpdateSecurityKeyCommand : AbstractRequestAcceptedCommand
    {
        public UpdateSecurityKeyCommand(string requestId) : base(requestId)
        {
        }
    }
}
