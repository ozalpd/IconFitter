using IconLib.ViewModels;
using IconLib.Works;
using System;

namespace IconLib.Commands
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
                OptimizeTarget = ViewModel.OptimizeTarget,
                TargetExtension = ViewModel.TargetExtension,
                ResizeQuality = ViewModel.TargetQuality,
                ResizeHeight = ViewModel.TargetHeight,
                ResizeWidth = ViewModel.TargetWidth
            };

            resize.Execute(ViewModel.TargetResizeFileName, ViewModel.ImageFile);

            ViewModel.ElapsedTime = DateTime.Now.Subtract(startTime);
        }
    }
}
