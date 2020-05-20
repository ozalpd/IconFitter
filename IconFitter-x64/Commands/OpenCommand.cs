using IconLib.Models;
using IconFitter.ViewModels;
using Microsoft.Win32;
using IconFitter.Models;
using IconLib.Works;
using System.Collections.ObjectModel;

namespace IconFitter.Commands
{
    public class OpenCommand : AbstractCommand
    {
        public OpenCommand(IconFitterVM viewModel, FileType fileType) : base(viewModel)
        {
            FileType = fileType;
        }

        public override void Execute(object parameter)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = Files.GetFilter(FileType);

            if (openFileDialog.ShowDialog() != true)
                return;

            switch (FileType)
            {
                case FileType.ImageFile:
                    ViewModel.ImageFile = new ImageFileInfo(openFileDialog.FileName);
                    break;

                case FileType.ResizeWorkSet:
                    var workSet = ResizeWorkSet.OpenFromFile(openFileDialog.FileName);
                    ViewModel.RecentResizeSetFile = openFileDialog.FileName;
                    ViewModel.ResizeWorks = new ObservableCollection<ResizeWork>(workSet);
                    break;

                default:
                    break;
            }
        }

        public FileType FileType { get; private set; }
    }
}
