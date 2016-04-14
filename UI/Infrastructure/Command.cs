using System;
using System.Windows.Input;

namespace UI.Infrastructure
{
    public class Command : ICommand
    {
        private readonly Func<object,bool> canExecutePredicate;
        private readonly Action<object> commandAction;

        public Command(Action<object> commandAction) : this(commandAction, o => true)
        {
        }

        public Command(Action<object> commandAction, Func<object, bool> canExecutePredicate)
        {
            this.commandAction = commandAction;
            this.canExecutePredicate = canExecutePredicate;
        }

        public bool CanExecute(object parameter)
        {
            return canExecutePredicate.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            commandAction.Invoke(parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}