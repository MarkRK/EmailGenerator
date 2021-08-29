using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmailGenerator.Command
{
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> _action;
        private readonly Func<object, bool> _canExecute;

        private event EventHandler CanExecuteChangedInternal;

        public DelegateCommand(Action action) : this((o) => action())
        { }

        public DelegateCommand(Action<Object> action) : this(action, (o) => true)
        { }

        public DelegateCommand(Action<Object> action, Func<object, bool> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        #region ICommand
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                this.CanExecuteChangedInternal += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
                this.CanExecuteChangedInternal -= value;
            }
        }
        #endregion

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChangedInternal != null)
            {
                CanExecuteChangedInternal(this, EventArgs.Empty);
            }
        }
    }
}
