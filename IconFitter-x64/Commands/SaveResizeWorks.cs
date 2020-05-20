using IconFitter.Models;
using IconFitter.ViewModels;
using IconLib.Works;
using Microsoft.Win32;

namespace IconFitter.Commands
{
    public class SaveResizeWorks : ImgWorkSetCommand
    {
        public SaveResizeWorks(IconFitterVM viewModel) : base(viewModel) { }

        public override void Execute(object parameter)
        {
            if(string.IsNullOrEmpty(ViewModel.RecentResizeSetFile))
            {
                var openFileDialog = new SaveFileDialog()
                {
                    DefaultExt = Files.GetExtension(FileType.ResizeWorkSet),
                    Filter = Files.GetFilter(FileType.ResizeWorkSet)
                };
                if (openFileDialog.ShowDialog() != true)
                    return;

                ViewModel.RecentResizeSetFile = openFileDialog.FileName;
            }

            var workSet = new ResizeWorkSet();
            workSet.AddRange(ViewModel.ResizeWorks);
            workSet.SaveToFile(ViewModel.RecentResizeSetFile);
        }
    }
}
