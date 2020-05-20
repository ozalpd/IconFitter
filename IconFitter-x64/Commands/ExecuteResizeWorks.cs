using IconFitter.ViewModels;
using IconLib.Models;
using IconLib.Works;
using System.ComponentModel;

namespace IconFitter.Commands
{
    public class ExecuteResizeWorks : ImgWorkSetCommand
    {
        public ExecuteResizeWorks(IconFitterVM viewModel) : base(viewModel) { }

        protected ImageFileInfo ImageFile { get { return ViewModel.ImageFile; } }

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter)
                && ImageFile != null && ImageFile.Exists
                && ImgWork.IsTargetExtSupported(ViewModel.TargetExtension);
        }

        public override void Execute(object parameter)
        {
            var workSet = new ResizeWorkSet();
            workSet.TargetDirectory = ViewModel.TargetDirectory;
            foreach (var resize in ViewModel.ResizeWorks)
            {
                var clone = resize.Clone();
                if (string.IsNullOrEmpty(clone.TargetFileName))
                    clone.TargetFileName = ViewModel.GetResizeFileName(clone);

                workSet.Add(clone);
            }
            workSet.Execute(ImageFile);
        }

        protected override void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnViewModelPropertyChanged(sender, e);
            if (e.PropertyName.Equals("ImageFile") ||
                e.PropertyName.Equals("TargetExtension") ||
                e.PropertyName.Equals("TargetFileName"))
            {
                RaiseCanExecuteChanged();
            }
        }
    }
}
