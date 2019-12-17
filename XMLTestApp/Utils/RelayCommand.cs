using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace XMLTestApp.Utils
{
    public class RelayCommand : ICommand
    {
        private Func<bool> _CanExecute;
        private Action Action;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action action, Func<bool> canExecute)
        {
            this.Action = action;
            this._CanExecute = canExecute;
        }
        public RelayCommand(Action action)
        {
            this.Action = action;
            this._CanExecute = () => true;
        }
        public bool CanExecute(object parameter)
        {
            bool result = this._CanExecute.Invoke();
            return result;
        }
        public void Execute(object parameter)
        {
            this.Action.Invoke();
        }
    }
}
