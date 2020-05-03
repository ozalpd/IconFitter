using ImageMagick;

namespace IconLib.Works
{
    public class Win32IconWork : OptimizeWork
    {
        public bool OptimizeTarget { get; set; }

        protected override void ExecuteWork()
        {
            using (var image = new MagickImage(ImageFile.FullName))
            {
                if (ImageFile.Width > ImageFile.Height)
                {
                    image.Crop(ImageFile.Height, ImageFile.Height, Gravity.Center);
                }
                else if (ImageFile.Height > ImageFile.Width)
                {
                    image.Crop(ImageFile.Width, ImageFile.Width, Gravity.Center);
                }


                image.Settings.SetDefine(MagickFormat.Icon, "auto-resize", "256,48,32,24,16");
                image.Write(TargetFile);
            }

            if (OptimizeTarget)
                base.ExecuteWork();
        }
    }
}
