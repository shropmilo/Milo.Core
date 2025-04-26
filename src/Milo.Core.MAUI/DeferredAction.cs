using NLog;

namespace Milo.Core.MAUI;

/// <summary>
/// Action to run at a deferred time - continued calls to Defer restarts the timer
/// </summary>
public class DeferredAction(Action action)
{
    private IDispatcherTimer? _dispatcherTimer;
    private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Run the action in a specified time, if another call is made into 
    /// </summary>
    /// <param name="time"></param>
    public void Defer(TimeSpan time)
    {
        if (_dispatcherTimer == null)
        {
            _dispatcherTimer = Application.Current?.Dispatcher.CreateTimer();
            if (_dispatcherTimer != null)
            {
                _dispatcherTimer.Tick += (sender, args) => OnExecute();
            }
        }

        if (_dispatcherTimer != null)
        {
            _dispatcherTimer.Stop();
            _dispatcherTimer.Interval = time;
            _dispatcherTimer.Start();
        }
        else
        {
            Logger.Warn("Unable to create DispatchTimer - calling action immediately");
            OnExecute();
        }
    }

    /// <summary>
    /// Calls the base 
    /// </summary>
    private void OnExecute()
    {
        if (_dispatcherTimer != null)
        {
            _dispatcherTimer.Stop();
            _dispatcherTimer = null;
        }

        try
        {
            action.Invoke();
        }
        catch (Exception e)
        {
            Logger.Error(e);
        }
    }
}