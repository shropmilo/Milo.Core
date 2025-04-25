using System.Text;

namespace Milo.Core.Encoding
{
    /// <summary>
    /// Service to encode / decode a strings against a key encoder 
    /// </summary>
    internal class DefaultEncodingService : IEncodingService
    {
        private readonly List<IEncoderKey> _encoderKeys = [];

        public void Start()
        {
            for (int i = 0; i < 100; i++)
            {
                _encoderKeys.Add(new DefaultEncoderKey(Guid.NewGuid()));
            }
        }

        public void Stop()
        {
            
        }

        /// <summary>
        /// Available keys
        /// </summary>
        public IEnumerable<IEncoderKey> Keys => _encoderKeys;

        /// <summary>
        /// Convert from an initial string into an encoded string based on the supplied key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public string EncodeMessage(IEncoderKey key, string? message)
        {
            var encMessage = new StringBuilder();

            if (message != null)
            {
                foreach (char c in message)
                {
                    encMessage.Append(key.Map[c]);
                }
            }

            return encMessage.ToString();
        }

        /// <summary>
        /// Turns an encoded message back to the original
        /// </summary>
        /// <param name="key"></param>
        /// <param name="encodedMessage"></param>
        /// <returns></returns>
        public string DecodeMessage(IEncoderKey key, string? encodedMessage)
        {
            var message = new StringBuilder();
            if (encodedMessage != null)
            {
                foreach (char c in encodedMessage)
                {
                    message.Append(key.Map.FirstOrDefault(kvp => kvp.Value == c).Key);
                }
            }

            return message.ToString();
        }
    }
}
