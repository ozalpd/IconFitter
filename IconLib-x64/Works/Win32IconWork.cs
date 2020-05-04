using IconLib.Models;
using ImageMagick;

namespace IconLib.Works
{
    public class Win32IconWork : OptimizeWork
    {
        public bool OptimizeTarget { get; set; }

        protected override void ExecuteWork(ImageFileInfo sourceImage, string fullTargetName)
        {
            using (var image = new MagickImage(sourceImage.FullName))
            {
                if (sourceImage.Width > sourceImage.Height)
                {
                    image.Crop(sourceImage.Height, sourceImage.Height, Gravity.Center);
                }
                else if (sourceImage.Height > sourceImage.Width)
                {
                    image.Crop(sourceImage.Width, sourceImage.Width, Gravity.Center);
                }


                image.Settings.SetDefine(MagickFormat.Icon, "auto-resize", "256,48,32,24,16");
                image.Write(fullTargetName);
            }

            if (OptimizeTarget)
                base.ExecuteWork(sourceImage, fullTargetName);
        }
    }
}
