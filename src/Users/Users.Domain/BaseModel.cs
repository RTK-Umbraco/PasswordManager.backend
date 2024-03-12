namespace PasswordManager.Users.Domain;
public class BaseModel
{
    public Guid Id { get; private protected init; }
    public DateTime CreatedUtc { get; protected init; }
    public DateTime ModifiedUtc { get; private protected init; }

    protected BaseModel(Guid id)
    {
        Id = id;
    }

    protected BaseModel()
    {

    }

    public BaseModel(Guid id, DateTime createdUtc, DateTime modifiedUtc)
    {
        Id = id;
        CreatedUtc = createdUtc;
        ModifiedUtc = modifiedUtc;
    }
}
