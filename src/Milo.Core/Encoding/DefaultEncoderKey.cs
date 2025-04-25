namespace Milo.Core.Encoding;

/// <summary>
/// Simple Char to Char lookup mapping that allows chars to be replaced with other chars to
/// provide a light scramble of a string.
/// </summary>
internal class DefaultEncoderKey : IEncoderKey
{
    /// <summary>
    /// Must be associated encoded string to allow for decoding back.
    /// </summary>
    public Guid UniqueKey { get; }

    /// <summary>
    /// Char to Char map
    /// </summary>
    public IDictionary<char, char> Map { get; }

    public DefaultEncoderKey(Guid uniqueKey, int max = char.MaxValue)
    {
        UniqueKey = uniqueKey;
        Map = GenerateKeyBase(max);
        PopulateKey(Map, max);
    }

    /// <summary>
    /// Populate the map dictionary with all possible chars that can be replaced.
    /// </summary>
    /// <param name="max"></param>
    /// <returns></returns>
    private static IDictionary<char, char> GenerateKeyBase(int max)
    {
        var map = new Dictionary<char, char>();
        foreach (var asciiCharacter in GetAsciiCharacters(max))
        {
            map.Add(asciiCharacter, char.MinValue);
        }

        return map;
    }

    /// <summary>
    /// Creates a random ordered list of char
    /// </summary>
    /// <param name="map"></param>
    /// <param name="max"></param>
    private static void PopulateKey(IDictionary<char, char> map, int max)
    {
        var r = new Random();
        var random = GetAsciiCharacters(max).OrderBy(x => r.Next());
        var index = 0;
        foreach (var key in random)
        {
            map[(char)index] = key;
            index++;
        }
    }

    /// <summary>
    /// Generate all chars
    /// </summary>
    /// <param name="max"></param>
    /// <returns></returns>
    private static IEnumerable<char> GetAsciiCharacters(int max = char.MaxValue)
    {
        for (int i = 0; i < max; i++)
        {
            yield return (char)i;
        }
    }
}