using IconFitter.ViewModels;
using IconLib.Works;
using System;
using System.IO;

namespace IconFitter.Commands
{
    public class OptimizeTargetCommand : AbstractImgWorkCommand
    {
        public OptimizeTargetCommand(IconFitterVM viewModel) : base(viewModel) { }


        public override void Execute(object parameter)
        {
            DateTime startTime = DateTime.Now;
            if (parameter is string && File.Exists((string)parameter))
            {
                string targetFile = (string)parameter;
                if (ImgWork.IsTargetExtSupported(Path.GetExtension(targetFile)) == false)
                    return;
            }
            else
            {
                if (ImgWork.IsTargetExtSupported(ViewModel.TargetExtension) == false)
                    return;
            }

            var work = new OptimizeWork()
            {
                TargetExtension = ViewModel.TargetExtension
            };
            work.Execute(ImageFile, ViewModel.TargetDirectory);

            ViewModel.ElapsedTime = DateTime.Now.Subtract(startTime);
        }
    }
}
