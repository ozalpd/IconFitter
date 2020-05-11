using IconFitter.ViewModels;
using IconLib.Works;

namespace IconFitter.Commands
{
    public class AppendResizeCommand : ResizeCommand
    {
        public AppendResizeCommand(IconFitterVM viewModel) : base(viewModel) { }


        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            ResizeWork resize = GetResizeWork();
            ViewModel.ResizeWorks.Add(resize);
        }
    }
}
