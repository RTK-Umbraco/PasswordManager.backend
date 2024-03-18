namespace KeyVaults.Messages.CreateSecurityKey
{
    public sealed class CreateSecurityKeyCommand : AbstractRequestAcceptedCommand
    {
        public CreateSecurityKeyCommand(string requestId) : base(requestId)
        {
        }
    }
}
