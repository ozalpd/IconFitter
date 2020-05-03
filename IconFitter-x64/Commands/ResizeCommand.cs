using IconFitter.ViewModels;
using IconLib.Works;
using System;

namespace IconFitter.Commands
{
    public class ResizeCommand : OptimizeTargetCommand
    {
        public ResizeCommand(IconFitterVM viewModel) : base(viewModel) { }

        public override bool CanExecute(object parameter)
        {
            return ImageFile != null && ImageFile.Exists;
        }

        public override void Execute(object parameter)
        {
            DateTime startTime = DateTime.Now;
            ViewModel.CreateTargetDirectory();

            var resize = new ResizeWork()
            {
                KeepAspectRatio = ViewModel.KeepAspectRatio,
                Optimize = ViewModel.OptimizeTarget,
                TargetExtension = ViewModel.TargetExtension,
                Quality = ViewModel.TargetQuality,
                Height = ViewModel.TargetHeight,
                Width = ViewModel.TargetWidth
            };

            resize.Execute(ViewModel.TargetResizeFileName, ViewModel.ImageFile);

            ViewModel.ElapsedTime = DateTime.Now.Subtract(startTime);
        }
    }
}
