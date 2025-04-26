namespace Milo.Core.Services;

/// <summary>
/// Core interface for service finding and instance creation
/// </summary>
public interface IMiloServiceManager
{
    /// <summary>
    /// Creates a new instance of the requested object - Lifetime is managed by the caller.
    /// </summary>
    /// <typeparam name="TInstanceType"></typeparam>
    /// <returns></returns>
    TInstanceType CreateInstance<TInstanceType>();

    /// <summary>
    /// Gets a singleton registered service.
    ///
    /// Lifetime is managed by Core.
    /// </summary>
    /// <typeparam name="TMiloService"></typeparam>
    /// <returns></returns>
    TMiloService GetService<TMiloService>() where TMiloService : IMiloService;

    /// <summary>
    /// Gets all singleton instances
    /// </summary>
    /// <typeparam name="TMiloService"></typeparam>
    /// <returns></returns>
    IEnumerable<TMiloService> GetServices<TMiloService>() where TMiloService : IMiloService;

    /// <summary>
    /// Register a new service
    /// </summary>
    /// <typeparam name="TMiloService"></typeparam>
    /// <param name="instance"></param>
    /// <returns></returns>
    bool RegisterService<TMiloService>(TMiloService instance) where TMiloService : IMiloService;
}