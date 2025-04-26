using Milo.Core.Services;
using NLog;

namespace Milo.Core;

/// <summary>
/// Core services setup
/// </summary>
public static class MiloCore 
{
    private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

    public static bool IsStarted { get; private set; }

    /// <summary>
    /// Access to services and instance creation.
    /// </summary>
    public static IMiloServiceManager? Services { get; private set; }

    /// <summary>
    /// Locate a required service from the main application service system
    /// </summary>
    /// <typeparam name="TMiloService"></typeparam>
    /// <returns></returns>
    public static TMiloService? GetService<TMiloService>() where TMiloService : IMiloService
    {
        if (Services == null)
            return default;

        return Services.GetService<TMiloService>();
    }

    /// <summary>
    /// Get all services
    /// </summary>
    /// <typeparam name="TMiloService"></typeparam>
    /// <returns></returns>
    public static IEnumerable<TMiloService>? GetServices<TMiloService>() where TMiloService : IMiloService
    {
        return Services?.GetServices<TMiloService>();
    }

    /// <summary>
    /// Start all services
    /// </summary>
    /// <returns></returns>
    public static bool Start(IMiloServiceManager? serviceManager)
    {
        if (!IsStarted)
        {
            Services = serviceManager;

            var services = GetServices<IMiloService>();
            if (services != null)
            {
                foreach (var service in services)
                {
                    try
                    {
                        service.Start();
                    }
                    catch (Exception e)
                    {
                        Logger.Error(e);
                    }
                }
            }
            
            IsStarted = true;
        }

        return IsStarted;
    }

    /// <summary>
    /// Stop all services
    /// </summary>
    /// <returns></returns>
    public static bool Shutdown()
    {
        if (IsStarted)
        {
            var services = GetServices<IMiloService>();
            if (services != null)
            {
                foreach (var service in services)
                {
                    try
                    {
                        service.Stop();
                    }
                    catch (Exception e)
                    {
                        Logger.Error(e);
                    }
                }
            }

            IsStarted = false;
        }

        return IsStarted;
    }
}