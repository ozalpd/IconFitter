using IconLib.Models;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace IconLib.Works
{
    public abstract class ImgWork
    {
        [JsonIgnore]
        protected ImageFileInfo SourceImage { get; set; }

        [JsonIgnore]
        protected string TargetFile { get; set; }

        /// <summary>
        /// Extension of resulting image file
        /// </summary>
        public string TargetExtension
        {
            get
            {
                if (string.IsNullOrEmpty(_targetExtension) && SourceImage == null)
                    return string.Empty;

                if (string.IsNullOrEmpty(_targetExtension))
                    return SourceImage.Extension;

                return _targetExtension;
            }
            set
            {
                _targetExtension = value.Trim();
                if (!_targetExtension.StartsWith("."))
                    _targetExtension = string.Format(".{0}", _targetExtension);
            }
        }
        private string _targetExtension;


        /// <summary>
        /// Executes work
        /// </summary>
        /// <param name="targetFile">Path of resulting image file</param>
        /// <param name="sourceImage">Source image file info</param>
        public void Execute(string targetFile, ImageFileInfo sourceImage)
        {
            TargetFile = targetFile;
            SourceImage = sourceImage;
            ExecuteWork();
        }
        protected abstract void ExecuteWork();


        protected bool IsTargetSupported()
        {
            return IsTargetExtSupported(Path.GetExtension(TargetFile));
        }

        public static bool IsTargetExtSupported(string extension)
        {
            return SupportedTargetExts.Contains(extension.ToLowerInvariant());
        }

        public static string[] SupportedTargetExts = new string[4]
        {
            ".jpeg",
            ".jpg",
            ".png",
            ".ico"
        };
    }
}
