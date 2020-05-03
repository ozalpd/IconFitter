using ImageMagick;

namespace IconLib.Works
{
    public class Win32IconWork : OptimizeWork
    {
        public bool OptimizeTarget { get; set; }

        protected override void ExecuteWork()
        {
            using (var image = new MagickImage(SourceImage.FullName))
            {
                if (SourceImage.Width > SourceImage.Height)
                {
                    image.Crop(SourceImage.Height, SourceImage.Height, Gravity.Center);
                }
                else if (SourceImage.Height > SourceImage.Width)
                {
                    image.Crop(SourceImage.Width, SourceImage.Width, Gravity.Center);
                }


                image.Settings.SetDefine(MagickFormat.Icon, "auto-resize", "256,48,32,24,16");
                image.Write(TargetFile);
            }

            if (OptimizeTarget)
                base.ExecuteWork();
        }
    }
}
