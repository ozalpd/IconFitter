using IconLib.ViewModels;
using System;
using System.Windows.Input;

namespace IconLib.Commands
{
    public abstract class AbstractCommand : ICommand
    {
        public AbstractCommand(IconFitterVM viewModel)
        {
            ViewModel = viewModel;
        }
        public IconFitterVM ViewModel { get; private set; }

        public abstract void Execute(object parameter);

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        protected virtual void RaiseCanExecuteChanged(object sender = null, EventArgs e = null)
        {
            if (sender == null)
                sender = this;
            if (e == null)
                e = new EventArgs();

            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(sender, e);
            }
        }
        public event EventHandler CanExecuteChanged;
    }
}
