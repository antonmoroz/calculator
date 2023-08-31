using System;
using System.Windows.Input;

namespace Calculator.Commands
{
    class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;

        private readonly Func<T, bool>? _canExecute;

        public RelayCommand(Action<T> execute) : this(execute, null) { }

        public RelayCommand(Action<T> execute, Func<T, bool>? canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object? parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }

            if (parameter == null)
            {
                return false;
            }

            return _canExecute((T)parameter);
        }

        public void Execute(object? parameter)
        {
            if (parameter != null && parameter is T p)
            {
                _execute(p);
            }
        }
    }
}