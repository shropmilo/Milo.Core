using Milo.Core.Commands;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Milo.Core.MAUI.Commands
{
    /// <summary>
    /// Command with a Header
    /// </summary>
    public class VisualCommand : Command, IMiloVisualCommand
    {
        private object? _header;

        public object? Header
        {
            get => _header;
            set
            {
                if (Equals(value, _header)) return;
                _header = value;
                OnPropertyChanged();
            }
        }

        public VisualCommand(Action<object> execute, Func<object, bool> canExecute, object header) : base(execute, canExecute)
        {
            Header = header;
        }

        public VisualCommand(Action<object> execute, object header) : base(execute)
        {
            Header = header;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        #endregion
    }
}
