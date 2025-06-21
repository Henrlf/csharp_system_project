namespace Franco.Core.Dto.Messaging;

public class Message
{
    public string MessageType {get; protected set;}
    
    protected Message()
    {
        MessageType = GetType().Name;
    }
}