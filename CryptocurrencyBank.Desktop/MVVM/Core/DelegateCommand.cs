using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptocurrencyBank.Desktop.MVVM.Core
{
    class DelegateCommand : ICommand
    {
        private Action<object?> _execute;
        private Func<object?, bool> _canExecute;

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested+= value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public DelegateCommand(Action<object?> execute, Func<object?, bool> canExecute)
            => (_execute, _canExecute) = (execute, canExecute);

        public bool CanExecute(object? parameter)
            => _canExecute(parameter);

        public void Execute(object? parameter)
            => _execute(parameter);
    }
}
