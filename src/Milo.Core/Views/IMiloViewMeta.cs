namespace Milo.Core.Views
{
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

    public interface IMiloViewMeta
    {
        Guid UniqueGuid { get; }

        object Header { get; }

        object Context { get; set; }
    }

    public abstract class MiloViewMetaBase : IMiloViewMeta
    {
        public abstract Guid UniqueGuid { get; }
        public abstract object Header { get; }
        public abstract object Context { get; set; }
    }

    public interface IMiloView
    {
        IMiloViewMeta Meta { get; }

        void Initialise(IMiloViewMeta meta);

        object Content { get; }
    }
}
