namespace Password.Messages;
public abstract class AbstractPasswordFailedEvent
{
    public string ErrorMessage { get; }

    protected AbstractPasswordFailedEvent(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }
}
