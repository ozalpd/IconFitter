﻿using IconFitter.ViewModels;
using IconLib.Works;
using System;

namespace IconFitter.Commands
{
    public class MakeWin32Icon : ImgWorkCommand
    {
        public MakeWin32Icon(IconFitterVM viewModel) : base(viewModel) { }


        public override void Execute(object parameter)
        {
            DateTime startTime = DateTime.Now;
            var work = new Win32IconWork()
            {
                OptimizeTarget = ViewModel.OptimizeTarget,
                TargetExtension = ".ico"
            };
            work.Execute(ImageFile, ViewModel.TargetDirectory);

            ViewModel.ElapsedTime = DateTime.Now.Subtract(startTime);
        }
    }
}
