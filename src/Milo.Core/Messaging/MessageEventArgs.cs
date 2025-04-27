namespace Milo.Core.Messaging;

public class MessageEventArgs : EventArgs
{
    public IMiloMessage Message { get; }

    public MessageEventArgs(IMiloMessage message)
    {
        Message = message;
    }
}