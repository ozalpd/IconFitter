using IconLib.ViewModels;
using IconLib.Works;
using System;
using System.IO;

namespace IconLib.Commands
{
    public class OptimizeTargetCommand : AbstractImageCommand
    {
        public OptimizeTargetCommand(IconFitterVM viewModel) : base(viewModel) { }

        public new IconFitterVM ViewModel { get => (IconFitterVM)base.ViewModel; }

        public override bool CanExecute(object parameter)
        {
            return ImageFile != null && ImageFile.Exists && ImgWork.IsExtSupported(ImageFile.Extension);
        }


        public override void Execute(object parameter)
        {
            DateTime startTime = DateTime.Now;
            if (parameter is string && File.Exists((string)parameter))
            {
                targetFile = (string)parameter;
                if (!IsSupported())
                    return;
            }
            else
            {
                if (!ImgWork.IsExtSupported(ViewModel.TargetExtension))
                    return;

                targetFile = ViewModel.TargetFileName;
            }

            var opt = new OptimizeWork();
            opt.TargetExtension = ViewModel.TargetExtension;
            opt.Execute(targetFile, ImageFile);

            ViewModel.ElapsedTime = DateTime.Now.Subtract(startTime);
        }
        private string targetFile;



        private bool IsSupported()
        {
            return ImgWork.IsExtSupported(Path.GetExtension(targetFile));
        }
    }
}
