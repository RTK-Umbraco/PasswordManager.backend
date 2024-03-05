namespace PasswordManager.Password.Infrastructure.BaseRepository;
public class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    public DateTime ModifiedUtc { get; set; } = DateTime.UtcNow;

    public BaseEntity(Guid id, DateTime createdUtc, DateTime modifiedUtc)
    {
        Id = id;
        CreatedUtc = createdUtc;
        ModifiedUtc = modifiedUtc;
    }
}
