using System;
using System.Windows.Input;

namespace UI.Infrastructure
{
    public class Command : ICommand
    {
        private bool executable = true;

        public event Action<object> OnExecute;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return executable;
        }

        public void Execute(object parameter = null)
        {
            OnExecute?.Invoke(parameter);
        }
    }
}