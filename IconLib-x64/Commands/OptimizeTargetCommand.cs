using IconLib.Commands;
using ImageMagick;
using IconLib.ViewModels;
using System;
using System.IO;
using System.Linq;

namespace IconLib.Commands
{
    public class OptimizeTargetCommand : AbstractImageCommand
    {
        public OptimizeTargetCommand(IconFitterVM viewModel) : base(viewModel) { }

        public new IconFitterVM ViewModel { get => (IconFitterVM)base.ViewModel; }

        public override bool CanExecute(object parameter)
        {
            return ImageFile != null && ImageFile.Exists && IsSupported(ImageFile.Extension);
        }


        public override void Execute(object parameter)
        {

            DateTime startTime = DateTime.Now;
            bool setElapsedTime = false;
            if (parameter is string && File.Exists((string)parameter))
            {
                targetFile = (string)parameter;
                if (!IsSupported())
                    return;
            }
            else if (!File.Exists(ViewModel.TargetFileName))
            {
                if (!IsSupported(ViewModel.TargetExtension))
                    return;

                targetFile = ViewModel.TargetFileName;
                ViewModel.CreateTargetDirectory();
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
                if (!IsSupported(ViewModel.TargetExtension))
                    return;

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
        private string targetFile;



        private bool IsSupported()
        {
            return IsSupported(Path.GetExtension(targetFile));
        }

        private bool IsSupported(string extension)
        {
            return supportedExts.Contains(extension.ToLowerInvariant());
        }

        private string[] supportedExts = new string[4]
        {
            ".jpeg",
            ".jpg",
            ".png",
            ".ico"
        };
    }
}
