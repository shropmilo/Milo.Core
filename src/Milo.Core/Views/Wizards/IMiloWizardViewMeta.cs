namespace Milo.Core.Views.Wizards;

public interface IMiloWizardViewMeta : IMiloViewMeta
{
    bool CanMoveNext(object context);

    bool CanMoveBack(object context);
}