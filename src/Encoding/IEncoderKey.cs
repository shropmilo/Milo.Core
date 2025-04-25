namespace Milo.Core.Encoding;

/// <summary>
/// Map of Char used when encoding
/// </summary>
public interface IEncoderKey
{
    /// <summary>
    /// Important to recreate a message based on the key
    /// </summary>
    Guid UniqueKey { get; }

    /// <summary>
    /// Map of Char to alternative Char
    /// </summary>
    IDictionary<char, char> Map { get; }
}