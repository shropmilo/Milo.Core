namespace Milo.Core.Views;

public interface IMiloView
{
     IMiloViewMeta Meta { get; }

    void Initialise(IMiloViewMeta meta);

    object Content { get; }
}