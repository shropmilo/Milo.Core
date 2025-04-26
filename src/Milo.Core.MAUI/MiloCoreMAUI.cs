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
            var ourAssemblies = new List<Assembly>
            {
                Assembly.GetExecutingAssembly(),
                Assembly.GetCallingAssembly()
            };

            var assemblies = new List<Assembly>(ourAssemblies);
            foreach (var ourAssembly in ourAssemblies)
            {
                foreach (var referencedAssembly in ourAssembly.GetReferencedAssemblies())
                {
                    try
                    {
                        var ass = Assembly.Load(referencedAssembly);
                        if(!assemblies.Contains(ass))
                            assemblies.Add(ass);
                    }
                    catch (Exception e)
                    {
                       Logger.Error(e);
                    }
                }
            }

            if (additional != null)
            {
                assemblies.AddRange(additional);
            }

            foreach (var assembly in assemblies)
            {
                foreach (var miloService in MiloCore.CreateInstances<IMiloService>(assembly))
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

    }
}
