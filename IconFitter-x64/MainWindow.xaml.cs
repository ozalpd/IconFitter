using IconLib.Models;
using IconLib.ViewModels;
using System.IO;
using System.Windows;
using System.Reflection;
using System;
using System.Windows.Threading;

namespace IconFitter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            string appDir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            settingsFile = System.IO.Path.Combine(appDir, "IconFitter.prefs.json");

            if (File.Exists(settingsFile))
            {
                _settings = AppSettings.OpenFromFile(settingsFile);
                _settings.MainWindowPosition.SetWindowPositions(this);
            }
            else
            {
                _settings = new AppSettings();
            }

            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }
        private AppSettings _settings;
        private string settingsFile;
        private bool imgAutoZoomed = false;
        public MainWindow(string startupFile) : this()
        {
            StartupFile = startupFile;
        }


        public string StartupFile { get; private set; }
        public IconFitterVM ViewModel { get { return (IconFitterVM)DataContext; } }
        public ImageFileInfo ImageFile { get { return ViewModel.ImageFile; } }


        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("ImageFile"))
            {
                FitImageToViewport();
                imgAutoZoomed = true; //TODO: set this false when zoom changed by user
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(StartupFile))
            {
                ViewModel.ImageFile = new ImageFileInfo(StartupFile);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _settings.MainWindowPosition.GetWindowPositions(this);
            _settings.SaveToFile(settingsFile);
        }

        private void BtnSetHeightToWidth_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.TargetHeight = ViewModel.TargetWidth;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (imgAutoZoomed && !resizing)
            {
                var timer = new DispatcherTimer()
                {
                    Interval = new TimeSpan(0, 0, 0, 0, 250)
                };

                timer.Tick += (o, args) =>
                {
                    FitImageToViewport();
                    timer.Stop();
                    resizing = false;
                };
                timer.Start();
                resizing = true;
            }
        }
        private bool resizing = false;


        private void FitImageToViewport()
        {
            if (ImageFile == null)
                return;

            double horzRate = (ImageViewScroll.ViewportWidth - 1) / (double)ImageFile.Width;
            double vertRate = (ImageViewScroll.ViewportHeight - 1) / (double)ImageFile.Height;
            double zoomFact = vertRate < horzRate ? vertRate : horzRate;

            if (zoomFact < 1.0)
            {
                ViewModel.Zoom = Math.Floor(zoomFact * 1000.0) / 10.0;
            }
            else if (ViewModel.Zoom < 100.0)
            {
                ViewModel.Zoom = 100.0;
            }
        }
    }
}
