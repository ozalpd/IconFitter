using ImageMagick;

namespace IconLib.Works
{
    public class ResizeWork : OptimizeWork
    {
        /// <summary>
        /// New height of resulting image file
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// New width of resulting image file
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Preserves aspect ratio of original image. Crops resulting image if it is necessary
        /// </summary>
        public bool KeepAspectRatio { get; set; }

        /// <summary>
        /// Optimize size of resulting image file
        /// </summary>
        public bool Optimize { get; set; }

        /// <summary>
        /// Quality of resulting image file
        /// </summary>
        public int Quality { get; set; }

        protected override void ExecuteWork()
        {
            using (var image = new MagickImage(SourceImage.FullName))
            {
                MagickGeometry size;
                bool cropIt = false;
                if (KeepAspectRatio && Width > 0 && Height > 0) //Keep Aspect Ratio by cropping
                {
                    double rateW = (double)Width / SourceImage.Width;
                    double rateH = (double)Height / SourceImage.Height;

                    size = rateH > rateW
                         ? new MagickGeometry((int)(SourceImage.Width * rateH), Height)
                         : rateH < rateW
                         ? new MagickGeometry(Width, (int)(SourceImage.Height * rateW))
                         : new MagickGeometry(Width, Height);

                    cropIt = (rateH < rateW) || (rateH > rateW);
                }
                else
                {
                    size = new MagickGeometry(Width, Height);
                }

                size.IgnoreAspectRatio = !KeepAspectRatio;

                image.Quality = Quality;
                //image.Settings.Compression = CompressionMethod.LosslessJPEG;
                if (Optimize)
                    image.Strip();

                image.FilterType = FilterType.Lanczos2Sharp; //this seems better to me in some downsampled images - ozalp 2019.08.31
                image.Resize(size);

                if (cropIt)
                {
                    image.Crop(Width, Height, Gravity.Center);
                }

                //TODO: add ResultImageSource property to IconFitterVM
                //ViewModel.ResultImageSource = image.ToBitmapSource();
                image.Write(TargetFile);
            }

            if (Optimize)
                base.ExecuteWork();
        }
    }
}
