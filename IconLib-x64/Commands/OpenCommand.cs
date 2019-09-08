using IconLib.Models;
using IconLib.ViewModels;
using Microsoft.Win32;

namespace IconLib.Commands
{
    public class OpenCommand : AbstractCommand
    {
        public OpenCommand(IconFitterVM viewModel) : base(viewModel) { }

        public override void Execute(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png;*.psd|" +
                                    "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                                    "Portable Network Graphic (*.png)|*.png";

            if (openFileDialog.ShowDialog() != true)
                return;

            ViewModel.ImageFile = new ImageFileInfo(openFileDialog.FileName);
        }
    }
}
