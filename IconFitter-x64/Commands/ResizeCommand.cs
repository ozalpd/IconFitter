using IconFitter.ViewModels;
using IconLib.Works;
using System;

namespace IconFitter.Commands
{
    public class ResizeCommand : ImgWorkCommand
    {
        public ResizeCommand(IconFitterVM viewModel) : base(viewModel) { }


        public override void Execute(object parameter)
        {
            DateTime startTime = DateTime.Now;
            ResizeWork resize = GetResizeWork();
            resize.TargetFileName = ViewModel.GetResizeFileName(resize);
            resize.Execute(ViewModel.ImageFile, ViewModel.TargetDirectory);

            ViewModel.ElapsedTime = DateTime.Now.Subtract(startTime);
        }

        protected ResizeWork GetResizeWork()
        {
            var resize = new ResizeWork()
            {
                KeepAspectRatio = ViewModel.KeepAspectRatio,
                Optimize = ViewModel.OptimizeTarget,
                Quality = ViewModel.TargetQuality,
                Height = ViewModel.TargetHeight,
                Width = ViewModel.TargetWidth,
                //SpaceAlternative = '_',
                TargetExtension = ViewModel.TargetExtension,
                //TargetFileName = ViewModel.ResizeFileName
            };
            return resize;
        }
    }
}
