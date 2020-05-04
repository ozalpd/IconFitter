using IconLib.Models;
using ImageMagick;
using System;
using System.IO;

namespace IconLib.Works
{
    public class OptimizeWork : ImgWork
    {
        protected override void ExecuteWork(ImageFileInfo sourceImage, string fullTargetName)
        {
            string ext = Path.GetExtension(fullTargetName);
            if (!IsTargetExtSupported(ext))
                return; //TODO:Buraya bir error uydurmak gerekir mi? 

            if (File.Exists(fullTargetName) == false)
            {
                if (sourceImage.Extension.Equals(ext, StringComparison.InvariantCultureIgnoreCase))
                {
                    File.Copy(sourceImage.FullName, fullTargetName);
                }
                else
                {
                    using (var image = new MagickImage(sourceImage.FullName))
                    {
                        image.Strip();
                        image.Write(fullTargetName);
                    }
                }
            }

            var imageOptimizer = new ImageOptimizer
            {
                OptimalCompression = true
            };
            imageOptimizer.LosslessCompress(fullTargetName);
        }
    }
}
