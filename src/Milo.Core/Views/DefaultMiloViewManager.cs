namespace Milo.Core.Views;

/// <summary>
/// Basic ViewManager service
///
///     Supplies <see cref="IMiloViewMeta"/> for objects that have a factory available
///     Creates <see cref="IMiloView"/>>
/// </summary>
public class DefaultMiloViewManager : IMiloViewManager
{
    public IEnumerable<IMiloViewFactory> Factories { get; private set; } = new List<IMiloViewFactory>();

    /// <summary>
    /// Query all factories returning any available for the supplied context.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public IEnumerable<IMiloViewMeta> GetMetaList(object context)
    {
        var metas = new List<IMiloViewMeta>();

        foreach (var factory in Factories.Where(f => f.IsAvailable(context)))
        {
            var meta = factory.CreateViewMeta();
            meta.Context = context;
            metas.Add(meta);
        }
        return metas;
    }

    public Task<TMiloView> CreateView<TMiloView>(IMiloViewMeta meta) where TMiloView : IMiloView
    {
        return Task.Run(() =>
        {
            var factory = Factories.FirstOrDefault(f => f.ViewMetaType == meta.GetType() && f.IsAvailable(meta.Context));
            if (factory != null)
            {
                return (TMiloView)factory.CreateView(meta);
            }

            throw new InvalidOperationException("Factories supply views - If code ends up here things are bad!");
        });
    }

    public void Start()
    {
        Factories = (List<IMiloViewFactory>) [..MiloCore.Services.CreateInstances<IMiloViewFactory>()];
    }

    public void Stop()
    {
            
    }
}