using ImageMagick;
using IconFitter64.ViewModels;
using System;

namespace IconFitter64.Commands
{
    public class MakeWin32Icon : OptimizeTargetCommand
    {
        public MakeWin32Icon(IconFitterVM viewModel) : base(viewModel) { }


        public override void Execute(object parameter)
        {
            DateTime startTime = DateTime.Now;
            ViewModel.CreateTargetDirectory();

            string targetExt = ViewModel.TargetExtension;
            ViewModel.TargetExtension = ".ico";

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


                image.Settings.SetDefine(MagickFormat.Icon, "auto-resize", "256,128,64,48,32,16");
                image.Write(ViewModel.TargetFileName);
            }

            if (ViewModel.OptimizeTarget)
                base.Execute(ViewModel.TargetFileName);


            ViewModel.TargetExtension = targetExt;
            ViewModel.ElapsedTime = DateTime.Now.Subtract(startTime);
        }
    }
}
