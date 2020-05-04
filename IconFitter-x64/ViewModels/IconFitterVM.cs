using IconLib;
using IconLib.Models;
using System;
using System.Drawing;
using System.IO;

namespace IconFitter.ViewModels
{
    public partial class IconFitterVM
    {

        private const int defaultJpegQuality = 90;
        private const int defaultResizeSize = 128; //default width or height for ResizeCommand
        private const double defaultZoom = 100;
        private const double minZoom = 0.1;
        private const double maxZoom = 10000.0;


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
                ResetZoom();
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


        public bool OptimizeTarget
        {
            get
            {
                if (_optimizeTarget == null)
                    _optimizeTarget = true;
                return _optimizeTarget.Value;
            }
            set
            {
                if (_optimizeTarget != value)
                {
                    _optimizeTarget = value;
                    RaisePropertyChanged("OptimizeTarget");
                }
            }
        }
        private bool? _optimizeTarget;



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
        public string GetTargetDirectory()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "IconFitter");
        }

        public string ResizeFileName
        {
            get
            {
                if (ImageFile == null)
                    return string.Empty;

                if (TargetWidth == TargetHeight)
                {
                    return string.Format("{0}-{1}",
                                       Path.GetFileNameWithoutExtension(ImageFile.Name),
                                       TargetHeight);
                }
                else
                {
                    return string.Format("{0}-{1}x{2}",
                                       Path.GetFileNameWithoutExtension(ImageFile.Name),
                                       TargetWidth,
                                       TargetHeight);
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
            get { return _targetHeight ?? defaultResizeSize; }
            set
            {
                _targetHeight = value;
                RaisePropertyChanged("TargetHeight");
            }
        }
        private int? _targetHeight;

        public int TargetWidth
        {
            get { return _targetWidth ?? defaultResizeSize; }
            set
            {
                _targetWidth = value;
                RaisePropertyChanged("TargetWidth");
            }
        }
        private int? _targetWidth;


        public double Zoom
        {
            get { return _zoom ?? defaultZoom; }
            set
            {
                if (_zoom == value)
                    return;

                _zoom = value < minZoom ? minZoom
                      : value > maxZoom ? maxZoom
                      : value;
                RaisePropertyChanged("Zoom");
                RaisePropertyChanged("ZoomText");
                RaisePropertyChanged("ZoomFactor");
                RaisePropertyChanged("ImageViewSize");
            }
        }
        private double? _zoom;

        public double ZoomFactor { get { return Zoom / 100.0; } }

        public string[] ZoomOptions { get; } = {
                    "2.5 %",
                    "5 %",
                    "12.5 %",
                    "25 %",
                    "33.3 %",
                    "50 %",
                    "66.67 %",
                    "100 %",
                    "200 %",
                    "400 %",
                    "800 %",
                    "1600 %"
                };

        public string ZoomText
        {
            get
            {
                if (Zoom > 0)
                    return string.Format("{0} %", Zoom);
                return string.Empty;
            }
            set
            {
                double z = value.GetZoom();
                if (z > 0)
                {
                    Zoom = z;
                    RaisePropertyChanged("ManuelZoom");
                }
                else
                {
                    RaisePropertyChanged("ZoomText");
                }
            }
        }


        public Size ImageViewSize
        {
            get
            {
                return ImageFile != null
                            ? new Size((int)Math.Ceiling(ImageFile.Width * ZoomFactor), (int)Math.Ceiling(ImageFile.Height * ZoomFactor))
                            : new Size(0, 0);
            }
        }


        public void ResetZoom()
        {
            _zoom = null;
            Zoom = defaultZoom;
        }
    }
}
