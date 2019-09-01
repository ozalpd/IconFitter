using IconLib.Commands;
using ImageMagick;
using IconFitter64.ViewModels;
using System;
using System.IO;

namespace IconFitter64.Commands
{
    public class OptimizeTargetCommand : AbstractImageCommand
    {
        public OptimizeTargetCommand(IconFitterVM viewModel) : base(viewModel) { }

        public new IconFitterVM ViewModel { get => (IconFitterVM)base.ViewModel; }

        public override void Execute(object parameter)
        {

            DateTime startTime = DateTime.Now;
            bool setElapsedTime = false;
            string targetFile;
            if (parameter is string && File.Exists((string)parameter))
            {
                targetFile = (string)parameter;
            }
            else if (!File.Exists(ViewModel.TargetFileName))
            {
                ViewModel.CreateTargetDirectory();
                targetFile = ViewModel.TargetFileName;
                setElapsedTime = true;
                if (ImageFile.Extension.Equals(ViewModel.TargetExtension, StringComparison.InvariantCultureIgnoreCase))
                {
                    File.Copy(ImageFile.FullName, targetFile);
                }
                else
                {
                    using (var image = new MagickImage(ImageFile.FullName))
                    {
                        image.Strip();
                        image.Write(targetFile);
                    }
                }
            }
            else
            {
                targetFile = ViewModel.TargetFileName;
                setElapsedTime = true;
            }

            var imageOptimizer = new ImageOptimizer
            {
                OptimalCompression = true
            };
            imageOptimizer.LosslessCompress(targetFile);

            if (setElapsedTime)
                ViewModel.ElapsedTime = DateTime.Now.Subtract(startTime);
        }
    }
}
