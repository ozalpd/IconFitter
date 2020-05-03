using ImageMagick;
using System;
using System.IO;

namespace IconLib.Works
{
    public class OptimizeWork : ImgWork
    {
        protected override void ExecuteWork()
        {
            string targetDirectory = Path.GetDirectoryName(TargetFile);
            if (!Directory.Exists(targetDirectory))
                Directory.CreateDirectory(targetDirectory);

            if (!IsTargetExtSupported(TargetExtension))
                return; //TODO:Buraya bir error uydurmak gerekir mi? 

            if (File.Exists(TargetFile) == false)
            {
                if (SourceImage.Extension.Equals(TargetExtension, StringComparison.InvariantCultureIgnoreCase))
                {
                    File.Copy(SourceImage.FullName, TargetFile);
                }
                else
                {
                    using (var image = new MagickImage(SourceImage.FullName))
                    {
                        image.Strip();
                        image.Write(TargetFile);
                    }
                }
            }

            var imageOptimizer = new ImageOptimizer
            {
                OptimalCompression = true
            };
            imageOptimizer.LosslessCompress(TargetFile);
        }
    }
}
