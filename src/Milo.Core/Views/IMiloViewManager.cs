using Milo.Core.Services;

namespace Milo.Core.Views;

public interface IMiloViewManager : IMiloService
{
    /// <summary>
    /// All registered factories
    /// </summary>
    IEnumerable<IMiloViewFactory> Factories { get; }

    /// <summary>
    /// Get any <see cref="IMiloViewMeta"/> available for the object
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    IEnumerable<IMiloViewMeta> GetMetaList(object context);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMiloView"></typeparam>
    /// <param name="meta"></param>
    /// <returns></returns>
    Task<TMiloView> CreateView<TMiloView>(IMiloViewMeta meta) where TMiloView : IMiloView;
}