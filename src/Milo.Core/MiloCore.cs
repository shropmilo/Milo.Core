using System.Reflection;
using Milo.Core.Services;
using NLog.Extensions.Logging;

namespace Milo.Core;

/// <summary>
/// Core services setup
/// </summary>
public static class MiloCore 
{
    public static bool IsStarted { get; private set; }

    /// <summary>
    /// Locate a required service from the main application service system
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    public static TService? GetService<TService>() where TService : class
    {
        return IPlatformApplication.Current?.Services.GetService<TService>();
    }

    /// <summary>
    /// Get all services
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    public static IEnumerable<TService>? GetServices<TService>() where TService : class
    {
        return IPlatformApplication.Current?.Services.GetServices<TService>();
    }

    /// <summary>
    /// MauiAppBuilder extension to automatically register IMiloServices.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="additional"></param>
    public static void ConfigureCoreMiloServices(this MauiAppBuilder builder, IEnumerable<Assembly>? additional = null)
    {
        // We use NLog so get things registered
        builder.Logging.AddNLog();

        // Automatically reflect over assemblies to find MiloServices
        var assemblies = new List<Assembly>
        {
            Assembly.GetExecutingAssembly()
        };

        if (additional != null)
        {
            assemblies.AddRange(additional);
        }

        foreach (var assembly in assemblies)
        {
            foreach (var miloService in CreateInstances<IMiloService>(assembly))
            {
                builder.Services.Add(new ServiceDescriptor(miloService.GetType(), miloService));

                foreach (var @interface in miloService.GetType().GetInterfaces())
                {
                    builder.Services.Add(new ServiceDescriptor(@interface, miloService));
                }
            }
        }


        Start();
    }

    /// <summary>
    /// Seeks implementations of an interface within an assembly - these are created and returned.
    /// </summary>
    /// <typeparam name="TInterface"></typeparam>
    /// <param name="assembly"></param>
    /// <returns></returns>
    public static IEnumerable<TInterface> CreateInstances<TInterface>(Assembly assembly) where TInterface : class
    {
        var interfaceType = typeof(TInterface);
        var classes = assembly.GetTypes()
            .Where(type => interfaceType.IsAssignableFrom(type) && type.IsClass)
            .ToList();

        var list = new List<TInterface>();

        foreach (var cls in classes)
        {
            if(cls.IsAbstract)
                continue;

            try
            {
                if (Activator.CreateInstance(cls) is TInterface item)
                {
                    list.Add(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        return list;
    }

    /// <summary>
    /// Start all services
    /// </summary>
    /// <returns></returns>
    public static bool Start()
    {
        if (!IsStarted)
        {
            var services = GetServices<IMiloService>();
            if (services != null)
            {
                foreach (var service in services)
                {
                    service.Start();
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
                    service.Stop();
                }
            }

            IsStarted = false;
        }

        return IsStarted;
    }
}