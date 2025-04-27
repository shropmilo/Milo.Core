using Milo.Core.Services;

namespace Milo.Core.Messaging
{
    public interface IMiloMessageService : IMiloService
    {
        event EventHandler<MessageEventArgs> MessageSent;
        
        bool Send(IMiloMessage message);
    }
}
