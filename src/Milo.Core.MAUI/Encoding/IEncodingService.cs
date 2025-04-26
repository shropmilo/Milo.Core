using Milo.Core.Services;

namespace Milo.Core.Encoding
{
    /// <summary>
    /// Service that provides a level of encoding of strings based on a simple mapper encoder
    /// </summary>
    public interface IEncodingService : IMiloService
    {
        /// <summary>
        /// Available keys
        /// </summary>
        IEnumerable<IEncoderKey> Keys { get; }

        /// <summary>
        /// Converts a message into an encoded string
        /// </summary>
        /// <param name="key">Key to use in encoding / decoding</param>
        /// <param name="message">Message to encode</param>
        /// <returns></returns>
        string EncodeMessage(IEncoderKey key, string message);

        /// <summary>
        /// Returns a decoded message
        /// </summary>
        /// <param name="key">Key to use in encoding / decoding</param>
        /// <param name="encodedMessage"></param>
        /// <returns></returns>
        string DecodeMessage(IEncoderKey key, string encodedMessage);
    }
}
