using IconLib.Commands;
using IconLib.Models;
using System;
using System.ComponentModel;
using System.IO;

namespace IconLib.ViewModels
{
    public class AbstractViewModel : INotifyPropertyChanged
    {
        //default thumb width or height
        private const int defaultThumbSize = 128;
        private const int defaultJpegQuality = 90;


        public ImageFileInfo ImageFile
        {
            get { return _imageFile; }
            set
            {
                if (_imageFile == value)
                    return;

                if (string.IsNullOrEmpty(TargetExtension)
                    || _imageFile.Extension.Equals(TargetExtension, StringComparison.Ordinal))
                    _targetExtension = string.Empty;

                _imageFile = value;
                RaisePropertyChanged("ImageFile");

                if (string.IsNullOrEmpty(TargetExtension))
                {
                    TargetExtension = ImageFile.Extension;
                }
            }
        }
        private ImageFileInfo _imageFile;

        public bool KeepAspectRatio
        {
            get
            {
                if (_keepAspectRatio == null)
                    _keepAspectRatio = true;
                return _keepAspectRatio.Value;
            }
            set
            {
                if (_keepAspectRatio != value)
                {
                    _keepAspectRatio = value;
                    RaisePropertyChanged("KeepAspectRatio");
                }
            }
        }
        private bool? _keepAspectRatio;


        public string TargetExtension
        {
            get { return _targetExtension; }
            set
            {
                _targetExtension = value.Trim();
                if (!_targetExtension.StartsWith("."))
                    _targetExtension = string.Format(".{0}", _targetExtension);

                RaisePropertyChanged("TargetExtension");
                RaisePropertyChanged("TargetResizeFileName");
                RaisePropertyChanged("TargetFileName");
            }
        }
        private string _targetExtension;

        public string TargetFileName
        {
            get
            {
                return ImageFile != null
                    ? Path.Combine(TargetDirectory,
                           string.Format("{0}{1}",
                                      Path.GetFileNameWithoutExtension(ImageFile.Name),
                                      TargetExtension))
                    : string.Empty;
            }
        }

        public string TargetDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(_targetDir))
                    _targetDir = GetTargetDirectory();
                return _targetDir;
            }
            set
            {
                _targetDir = value;
                RaisePropertyChanged("TargetDirectory");
                RaisePropertyChanged("TargetResizeFileName");
                RaisePropertyChanged("TargetFileName");
            }
        }
        private string _targetDir;

        /// <summary>
        /// Checks the TargetDirectory if it exists, if it doesn't creates it
        /// </summary>
        public void CreateTargetDirectory()
        {
            if (!Directory.Exists(TargetDirectory))
                Directory.CreateDirectory(TargetDirectory);
        }

        /// <summary>
        /// Gets a default TargetDirectory
        /// </summary>
        /// <returns></returns>
        public virtual string GetTargetDirectory()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        public string TargetResizeFileName
        {
            get
            {
                if (ImageFile == null)
                    return string.Empty;

                if (TargetWidth == TargetHeight)
                {
                    return Path.Combine(TargetDirectory,
                            string.Format("{0}-{1}{2}",
                                       Path.GetFileNameWithoutExtension(ImageFile.Name),
                                       TargetHeight,
                                       TargetExtension));
                }
                else
                {
                    return Path.Combine(TargetDirectory,
                            string.Format("{0}-{1}x{2}{3}",
                                       Path.GetFileNameWithoutExtension(ImageFile.Name),
                                       TargetWidth,
                                       TargetHeight,
                                       TargetExtension));
                }
            }
        }

        public int TargetQuality
        {
            get { return _targQuality ?? defaultJpegQuality; }
            set
            {
                _targQuality = value;
                RaisePropertyChanged("TargetQuality");
            }
        }
        private int? _targQuality;


        public int TargetHeight
        {
            get { return _targetHeight ?? defaultThumbSize; }
            set
            {
                _targetHeight = value;
                RaisePropertyChanged("TargetHeight");
            }
        }
        private int? _targetHeight;

        public int TargetWidth
        {
            get { return _targetWidth ?? defaultThumbSize; }
            set
            {
                _targetWidth = value;
                RaisePropertyChanged("TargetWidth");
            }
        }
        private int? _targetWidth;


        public TimeSpan? ElapsedTime
        {
            get { return _elapsedTime; }
            set
            {
                _elapsedTime = value;
                RaisePropertyChanged("ElapsedTime");
            }
        }
        private TimeSpan? _elapsedTime;

        public OpenCommand OpenCommand
        {
            get
            {
                if (_openCommand == null)
                    _openCommand = GetOpenCommand();
                return _openCommand;
            }
        }
        private OpenCommand _openCommand;

        protected virtual OpenCommand GetOpenCommand()
        {
            return new OpenCommand(this);
        }


        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (!string.IsNullOrEmpty(propertyName) && PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
