using IconFitter.ViewModels;
using IconLib.Works;
using System;

namespace IconFitter.Commands
{
    public class ResizeCommand : AbstractImgWorkCommand
    {
        public ResizeCommand(IconFitterVM viewModel) : base(viewModel) { }


        public override void Execute(object parameter)
        {
            DateTime startTime = DateTime.Now;
            var resize = new ResizeWork()
            {
                KeepAspectRatio = ViewModel.KeepAspectRatio,
                Optimize = ViewModel.OptimizeTarget,
                Quality = ViewModel.TargetQuality,
                Height = ViewModel.TargetHeight,
                Width = ViewModel.TargetWidth,
                //SpaceAlternative = '_',
                TargetExtension = ViewModel.TargetExtension,
                TargetFileName = ViewModel.ResizeFileName
            };
            resize.Execute(ViewModel.ImageFile, ViewModel.TargetDirectory);

            ViewModel.ElapsedTime = DateTime.Now.Subtract(startTime);
        }
    }
}
