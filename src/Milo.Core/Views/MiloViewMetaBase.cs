namespace Milo.Core.Views;

public abstract class MiloViewMetaBase : IMiloViewMeta
{
    public abstract Guid UniqueGuid { get; }
    public abstract object Header { get; }
    public abstract object Context { get; set; }
}