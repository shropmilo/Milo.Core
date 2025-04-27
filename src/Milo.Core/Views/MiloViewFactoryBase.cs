namespace Milo.Core.Views;

public abstract class MiloViewFactoryBase : IMiloViewFactory
{
    public abstract Guid UniqueGuid { get; }
    public abstract object Header { get; }
    public abstract Type ViewType { get; }
    public abstract Type ViewMetaType { get; }

    public virtual IMiloViewMeta CreateViewMeta()
    {
        return (IMiloViewMeta)MiloCore.Services.CreateInstance(ViewMetaType);
    }

    public virtual IMiloView CreateView(IMiloViewMeta meta)
    {
        var view = (IMiloView)MiloCore.Services.CreateInstance(ViewType);
        view.Initialise(meta);
        return view;
    }

    public abstract bool IsAvailable(object parameter);
}