using IconFitter.ViewModels;
using System.ComponentModel;
using System.Linq;

namespace IconFitter.Commands
{
    public abstract class ImgWorkSetCommand : AbstractCommand
    {
        public ImgWorkSetCommand(IconFitterVM viewModel) : base(viewModel)
        {
            viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return ViewModel.ResizeWorks != null && ViewModel.ResizeWorks.Any();
        }


        protected virtual void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("ResizeWorks"))
                RaiseCanExecuteChanged();
        }
    }
}
