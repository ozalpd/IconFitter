﻿using IconFitter.Models;
using IconFitter.ViewModels;
using IconLib.Models;
using IconLib.Works;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
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

            string appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            settingsFile = Path.Combine(appDir, "IconFitter.prefs.json");

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
                imgAutoZoomed = true;
            }
            else if (e.PropertyName.Equals("ManuelZoom"))
            {
                imgAutoZoomed = false;
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

        private void btnDown_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedResizeWork == null || !(ViewModel.ResizeWorks.Count > 0))
                return;

            var work = ViewModel.SelectedResizeWork;
            int idx = ViewModel.ResizeWorks.IndexOf(work);
            idx++;
            if (idx < ViewModel.ResizeWorks.Count)
            {
                ViewModel.ResizeWorks.Move(idx - 1, idx);
                ViewModel.SelectedResizeWork = work;
            }
        }

        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedResizeWork == null || !(ViewModel.ResizeWorks.Count > 0))
                return;

            var work = ViewModel.SelectedResizeWork;
            int idx = ViewModel.ResizeWorks.IndexOf(work);
            if (idx > 0)
            {
                ViewModel.ResizeWorks.Move(idx, idx - 1);
                ViewModel.SelectedResizeWork = work;
            }
        }

        private void BtnSetHeightToWidth_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.TargetHeight = ViewModel.TargetWidth;
        }

        private void GridSplitter_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            onResizing();
        }

        private void GridSplitter_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            onResizing();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            onResizing();
        }

        private void onResizing()
        {
            if (imgAutoZoomed == false || resizing)
                return;

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
