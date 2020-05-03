using ImageMagick;

namespace IconLib.Works
{
    public class ResizeWork : OptimizeWork
    {
        public bool KeepAspectRatio { get; set; }
        public bool OptimizeTarget { get; set; }
        public int ResizeQuality { get; set; }
        public int ResizeHeight { get; set; }
        public int ResizeWidth { get; set; }

        protected override void ExecuteWork()
        {
            using (var image = new MagickImage(ImageFile.FullName))
            {
                MagickGeometry size;
                bool cropIt = false;
                if (KeepAspectRatio && ResizeWidth > 0 && ResizeHeight > 0) //Keep Aspect Ratio by cropping
                {
                    double rateW = (double)ResizeWidth / ImageFile.Width;
                    double rateH = (double)ResizeHeight / ImageFile.Height;

                    size = rateH > rateW
                         ? new MagickGeometry((int)(ImageFile.Width * rateH), ResizeHeight)
                         : rateH < rateW
                         ? new MagickGeometry(ResizeWidth, (int)(ImageFile.Height * rateW))
                         : new MagickGeometry(ResizeWidth, ResizeHeight);

                    cropIt = (rateH < rateW) || (rateH > rateW);
                }
                else
                {
                    size = new MagickGeometry(ResizeWidth, ResizeHeight);
                }

                size.IgnoreAspectRatio = !KeepAspectRatio;

                image.Quality = ResizeQuality;
                //image.Settings.Compression = CompressionMethod.LosslessJPEG;
                if (OptimizeTarget)
                    image.Strip();

                image.FilterType = FilterType.Lanczos2Sharp; //this seems better to me in some downsampled images - ozalp 2019.08.31
                image.Resize(size);

                if (cropIt)
                {
                    image.Crop(ResizeWidth, ResizeHeight, Gravity.Center);
                }

                //TODO: add ResultImageSource property to IconFitterVM
                //ViewModel.ResultImageSource = image.ToBitmapSource();
                image.Write(TargetFile);
            }

            if (OptimizeTarget)
                base.ExecuteWork();
        }
    }
}
