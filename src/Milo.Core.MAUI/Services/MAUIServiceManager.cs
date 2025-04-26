using Milo.Core.Services;
using NLog;

namespace Milo.Core.MAUI.Services
{
    public class MAUIServiceManager : IMiloServiceManager
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Standard Activator.CreateInstance
        /// </summary>
        /// <typeparam name="TInstanceType"></typeparam>
        /// <returns></returns>
        public TInstanceType? CreateInstance<TInstanceType>()
        {
            try
            {
                return Activator.CreateInstance<TInstanceType>();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            return default;
        }

        /// <summary>
        /// Returns a registered <see cref="IMiloService"/>
        /// </summary>
        /// <typeparam name="TMiloService"></typeparam>
        /// <returns></returns>
        public TMiloService? GetService<TMiloService>() where TMiloService : IMiloService
        {
            if (IPlatformApplication.Current != null)
            {
                var service = IPlatformApplication.Current.Services.GetService<TMiloService>();
                if (service == null)
                {
                    Logger.Warn($"Unable to locate service {typeof(TMiloService)}");
                }
                return service;
            }
            return default;
        }

        public IEnumerable<TMiloService> GetServices<TMiloService>() where TMiloService : IMiloService
        {
            if (IPlatformApplication.Current != null)
            {
                return IPlatformApplication.Current.Services.GetServices<TMiloService>();
            }
            return [];
        }

        public void Start()
        {
           
        }

        public void Stop()
        {
            
        }
    }
}
