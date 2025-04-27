namespace Milo.Core.Messaging;

public interface IMiloMessage
{
    MiloMessageType MessageType { get; }

    string Message { get; }
}