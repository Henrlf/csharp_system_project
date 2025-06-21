namespace Franco.Core.Dto.Messaging;

public abstract class Event: Message
{
    public DateTime Timestamp {get; private set;}

    protected Event()
    {
        Timestamp = DateTime.UtcNow;
    }
}