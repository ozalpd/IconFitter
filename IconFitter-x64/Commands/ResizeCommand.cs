using System;
using ImageMagick;
using IconFitter64.ViewModels;

namespace IconFitter64.Commands
{
    public class ResizeCommand : OptimizeTargetCommand
    {
        public ResizeCommand(IconFitterVM viewModel) : base(viewModel) { }

        public override void Execute(object parameter)
        {
            DateTime startTime = DateTime.Now;
            ViewModel.CreateTargetDirectory();

            using (var image = new MagickImage(ImageFile.FullName))
            {
                MagickGeometry size;
                bool cropIt = false;
                if (ViewModel.KeepAspectRatio && ViewModel.TargetWidth > 0 && ViewModel.TargetHeight > 0) //Keep Aspect Ratio by cropping
                {
                    double rateW = (double)ViewModel.TargetWidth / ImageFile.Width;
                    double rateH = (double)ViewModel.TargetHeight / ImageFile.Height;

                    size = rateH > rateW
                         ? new MagickGeometry((int)(ImageFile.Width * rateH), ViewModel.TargetHeight)
                         : rateH < rateW
                         ? new MagickGeometry(ViewModel.TargetWidth, (int)(ImageFile.Height * rateW))
                         : new MagickGeometry(ViewModel.TargetWidth, ViewModel.TargetHeight);

                    cropIt = (rateH < rateW) || (rateH > rateW);
                }
                else
                {
                    size = new MagickGeometry(ViewModel.TargetWidth, ViewModel.TargetHeight);
                }

                size.IgnoreAspectRatio = !ViewModel.KeepAspectRatio;

                image.Quality = ViewModel.TargetQuality;
                //image.Settings.Compression = CompressionMethod.LosslessJPEG;
                image.Strip();
                image.FilterType = FilterType.Lanczos2Sharp; //this seems better to me in some downsampled images - ozalp 2019.08.31
                image.Resize(size);

                if (cropIt)
                {
                    image.Crop(ViewModel.TargetWidth, ViewModel.TargetHeight, Gravity.Center);
                }

                image.Write(ViewModel.TargetResizeFileName);
            }

            if (ViewModel.OptimizeTarget)
                base.Execute(ViewModel.TargetResizeFileName);

            ViewModel.ElapsedTime = DateTime.Now.Subtract(startTime);
        }
    }
}
