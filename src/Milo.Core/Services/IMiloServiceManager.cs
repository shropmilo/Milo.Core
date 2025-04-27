namespace Milo.Core.Services;

/// <summary>
/// Core interface for service finding and instance creation
/// </summary>
public interface IMiloServiceManager : IMiloService
{
    /// <summary>
    /// Creates a new instance of the requested object - Lifetime is managed by the caller.
    /// </summary>
    /// <typeparam name="TInstanceType"></typeparam>
    /// <returns></returns>
    TInstanceType? CreateInstance<TInstanceType>() where TInstanceType : class;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    object CreateInstance(Type type);

    /// <summary>
    /// Returns a new instance of any object matching
    /// </summary>
    /// <typeparam name="TInstanceType"></typeparam>
    /// <returns></returns>
    IEnumerable<TInstanceType> CreateInstances<TInstanceType>() where TInstanceType : class;

    /// <summary>
    /// Gets a singleton registered service.
    ///
    /// Lifetime is managed by Core.
    /// </summary>
    /// <typeparam name="TMiloService"></typeparam>
    /// <returns></returns>
    TMiloService? GetService<TMiloService>() where TMiloService : IMiloService;

    /// <summary>
    /// Gets all singleton instances
    /// </summary>
    /// <typeparam name="TMiloService"></typeparam>
    /// <returns></returns>
    IEnumerable<TMiloService> GetServices<TMiloService>() where TMiloService : IMiloService;

}