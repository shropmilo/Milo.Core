namespace Milo.Core.Views;

public interface IMiloViewLifeCycleEvents
{
    void OnViewActivate();

    void OnViewDeactivate();

    void OnViewDestroy();
}