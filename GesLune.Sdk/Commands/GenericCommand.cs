using System.Windows.Input;

namespace GesLune.Sdk.Commands
{
    public class GenericCommand<T>(Action<T> execute, Predicate<T>? canExecute = null) : ICommand
    {
        private readonly Action<T> _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        private readonly Predicate<T>? _canExecute = canExecute;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            ArgumentNullException.ThrowIfNull(parameter, nameof(parameter));
            // If no canExecute predicate is provided, the command can always execute
            return _canExecute == null || _canExecute((T)parameter);
        }

        public void Execute(object? parameter)
        {
            ArgumentNullException.ThrowIfNull(parameter, nameof(parameter));
            _execute((T)parameter);
        }
    }
}