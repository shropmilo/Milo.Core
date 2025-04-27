using Milo.Core.Services;

namespace Milo.Core.BootStrap
{
    public interface IMiloBootStrapApplication : IMiloService
    {
        string Name { get; }

        event EventHandler Started;
    }
}
