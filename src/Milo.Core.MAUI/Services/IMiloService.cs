namespace Milo.Core.Services;

/// <summary>
/// Base identification interface allowing for automatic loading of IMiloServices
/// </summary>
public interface IMiloService
{
    /// <summary>
    /// Starts the service
    /// </summary>
    void Start();

    /// <summary>
    /// Ends service typically on shutdown
    /// </summary>
    void Stop();
}