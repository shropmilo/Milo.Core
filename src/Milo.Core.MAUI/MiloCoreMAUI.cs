using System.Reflection;
using Milo.Core.Services;
using NLog;
using NLog.Extensions.Logging;

namespace Milo.Core.MAUI
{
    public static class MiloCoreMAUI
    {
        /// <summary>
        /// NLog for trace and error capture
        /// </summary>
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// MauiAppBuilder extension to automatically register IMiloServices.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="additional"></param>
        public static MauiAppBuilder ConfigureCoreMiloServices(this MauiAppBuilder builder, IEnumerable<Assembly>? additional = null)
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
                    builder.Services.AddSingleton(miloService);

                    foreach (var @interface in miloService.GetType().GetInterfaces())
                    {
                        builder.Services.AddSingleton(@interface, miloService);
                    }
                }
            }

            return builder;
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
                if (cls.IsAbstract)
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
                    Logger.Error(e);
                }
            }

            return list;
        }
    }


}
