using System.ComponentModel;
using System.Windows.Input;

namespace Milo.Core.Commands
{
    public interface IMiloVisualCommand : ICommand, INotifyPropertyChanged
    {
        /// <summary>
        /// Command visual header
        /// </summary>
        public object? Header { get; }

        /// <summary>
        /// Check if a command should be available
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool IsAvailable(object parameter);
    }
}
