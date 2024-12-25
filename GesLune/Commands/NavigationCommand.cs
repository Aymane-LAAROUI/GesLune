using System.Windows.Input;

namespace GesLune.Commands
{
    public class NavigationCommand(Action<object?> execute, Predicate<object?> canExecute) : ICommand
    {
        private readonly Action<object?> _execute = execute;
        private readonly Predicate<object?> _canExecute = canExecute;

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            //ArgumentNullException.ThrowIfNull(parameter);
             return _canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            //ArgumentNullException.ThrowIfNull(parameter);
            _execute(parameter);
        }

        public void OnCanExecuteChanged() 
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
