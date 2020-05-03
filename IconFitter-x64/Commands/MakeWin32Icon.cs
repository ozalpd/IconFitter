using IconFitter.ViewModels;
using IconLib.Works;
using System;

namespace IconFitter.Commands
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

            var work = new Win32IconWork()
            {
                OptimizeTarget = ViewModel.OptimizeTarget,
                TargetExtension = ".ico"
            };
            work.Execute(ViewModel.TargetFileName, ImageFile);

            ViewModel.TargetExtension = targetExt;
            ViewModel.ElapsedTime = DateTime.Now.Subtract(startTime);
        }
    }
}
