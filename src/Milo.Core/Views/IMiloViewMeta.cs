namespace Milo.Core.Views
{
    public interface IMiloViewMeta
    {
        Guid UniqueGuid { get; }

        object Header { get; }

        object Context { get; set; }
    }
}
