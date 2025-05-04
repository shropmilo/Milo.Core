namespace Milo.Core.Views;

public interface IMiloViewFactory
{
    Guid UniqueGuid { get; }

    object Header { get; }

    Type ViewType { get; }

    Type ViewMetaType { get; }

    IMiloViewMeta CreateViewMeta();

    IMiloView CreateView(IMiloViewMeta meta);

    bool IsAvailable(object parameter);
}