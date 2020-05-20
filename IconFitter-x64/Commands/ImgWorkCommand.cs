using IconLib.Models;
using IconFitter.ViewModels;
using System.ComponentModel;
using IconLib.Works;

namespace IconFitter.Commands
{
    public abstract class AbstractImgWorkCommand : AbstractCommand
    {
        protected AbstractImgWorkCommand(IconFitterVM viewModel) : base(viewModel)
        {
            viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        protected ImageFileInfo ImageFile { get { return ViewModel.ImageFile; } }

        public override bool CanExecute(object parameter)
        {
            return ImageFile != null && ImageFile.Exists
                && ImgWork.IsTargetExtSupported(ViewModel.TargetExtension);
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
