using System.Diagnostics;
using Milo.Core.Services;
using NLog;
using System.Reflection;

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
    /// Start all services
    /// </summary>
    /// <returns></returns>
    public static bool Start(IMiloServiceManager serviceManager)
    {
        Debug.Assert(serviceManager != null);

        if (!IsStarted)
        {
            Services = serviceManager;
            var services = Services.GetServices<IMiloService>();
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
        if (IsStarted && Services != null)
        {
            var services = Services.GetServices<IMiloService>();
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

            IsStarted = false;
        }

        return IsStarted;
    }

    /// <summary>
    /// Seeks implementations of an interface within an assembly - these are created and returned.
    /// </summary>
    /// <typeparam name="TInterface"></typeparam>
    /// <param name="assembly"></param>
    /// <returns></returns>
    public static IEnumerable<TInterface> GetAssemblyInstances<TInterface>(Assembly assembly) where TInterface : class
    {
        var interfaceType = typeof(TInterface);
        var classes = assembly.GetTypes().Where(t => t is { IsClass: true, IsAbstract: false });
        var list = new List<TInterface>();

        foreach (var cls in classes)
        {
            if (interfaceType.IsAssignableFrom(cls))
            {
                try
                {
                    if (Activator.CreateInstance(cls) is TInterface item)
                    {
                        list.Add(item);
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            }
        }

        return list;
    }
}