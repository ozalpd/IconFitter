using IconFitter.ViewModels;
using IconLib.Works;
using System;
using System.IO;

namespace IconFitter.Commands
{
    public class OptimizeTargetCommand : AbstractImageCommand
    {
        public OptimizeTargetCommand(IconFitterVM viewModel) : base(viewModel) { }

        public new IconFitterVM ViewModel { get => (IconFitterVM)base.ViewModel; }

        public override bool CanExecute(object parameter)
        {
            return ImageFile != null && ImageFile.Exists
                && ImgWork.IsTargetExtSupported(ViewModel.TargetExtension);
        }


        public override void Execute(object parameter)
        {
            DateTime startTime = DateTime.Now;
            if (parameter is string && File.Exists((string)parameter))
            {
                targetFile = (string)parameter;
                if (ImgWork.IsTargetExtSupported(Path.GetExtension(targetFile)) == false)
                    return;
            }
            else
            {
                if (ImgWork.IsTargetExtSupported(ViewModel.TargetExtension) == false)
                    return;

                targetFile = ViewModel.TargetFileName;
            }

            var work = new OptimizeWork() { TargetExtension = ViewModel.TargetExtension };
            work.Execute(targetFile, ImageFile);

            ViewModel.ElapsedTime = DateTime.Now.Subtract(startTime);
        }
        private string targetFile;
    }
}
