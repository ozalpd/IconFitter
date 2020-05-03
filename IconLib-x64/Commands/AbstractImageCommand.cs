using IconLib.Models;
using IconLib.ViewModels;
using System.ComponentModel;

namespace IconLib.Commands
{
    public abstract class AbstractImageCommand : AbstractCommand
    {
        public AbstractImageCommand(IconFitterVM viewModel) : base(viewModel)
        {
            viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        protected ImageFileInfo ImageFile { get { return ViewModel.ImageFile; } }

        public override bool CanExecute(object parameter)
        {
            return ImageFile != null && ImageFile.Exists;
        }

        protected virtual void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("ImageFile") ||
                e.PropertyName.Equals("TargetExtension") ||
                e.PropertyName.Equals("TargetFileName"))
            {
                RaiseCanExecuteChanged();
            }
        }
    }
}
