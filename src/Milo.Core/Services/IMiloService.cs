namespace Milo.Core.Services
{
    /// <summary>
    /// Root interface for automatic service handling.
    /// </summary>
    public interface IMiloService
    {
        /// <summary>
        /// Core calls to set up a service
        /// </summary>
        void Start();

        /// <summary>
        /// Clean up a service
        /// </summary>
        void Stop();
    }
}
