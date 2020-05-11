using IconLib.Models;
using Newtonsoft.Json;
using System.Dynamic;
using System.IO;
using System.Linq;

namespace IconLib.Works
{
    public abstract class ImgWork
    {
        /// <summary>
        /// Target file name without extension
        /// </summary>
        public string TargetFileName { get; set; }

        /// <summary>
        /// Extension of resulting image file
        /// </summary>
        public string TargetExtension
        {
            get { return _targetExtension; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _targetExtension = string.Empty;
                    return;
                }

                if (value.Equals(_targetExtension))
                    return;

                _targetExtension = value.Trim();
                if (!_targetExtension.StartsWith("."))
                    _targetExtension = string.Format(".{0}", _targetExtension);
            }
        }
        private string _targetExtension;

        /// <summary>
        /// Sub folder name to use when calculating full path of resulting image file
        /// </summary>
        public string TargetSubFolder { get; set; }

        /// <summary>
        /// Alternative of space char.
        /// </summary>
        public char? SpaceAlternative { get; set; }

        public void Execute(ImageFileInfo sourceImage, string targetDirectory)
        {
            ExecuteWork(sourceImage, GetFullTargetPath(sourceImage, targetDirectory));
        }

        /// <summary>
        /// Executes work
        /// </summary>
        /// <param name="sourceImage">Source image file info</param>
        /// <param name="fullTargetName">Full path of resulting image file</param>
        protected abstract void ExecuteWork(ImageFileInfo sourceImage, string fullTargetName);

        protected virtual string GetFullTargetPath(ImageFileInfo sourceImage, string targetDirectory)
        {
            if (string.IsNullOrWhiteSpace(TargetSubFolder) == false)
                targetDirectory = Path.Combine(targetDirectory, TargetSubFolder);

            if (!Directory.Exists(targetDirectory))
                Directory.CreateDirectory(targetDirectory);

            return Path.Combine(targetDirectory, GetTargetFileName(sourceImage));
        }

        protected virtual string GetTargetFileName(ImageFileInfo sourceImage)
        {
            string ext = string.IsNullOrEmpty(TargetExtension)
                       ? sourceImage.Extension
                       : TargetExtension;

            string fileName = string.IsNullOrEmpty(TargetFileName)
                            ? string.Concat(Path.GetFileNameWithoutExtension(sourceImage.Name), ext)
                            : string.Concat(TargetFileName, ext);

            return SpaceAlternative.HasValue
                 ? fileName.Replace(' ', SpaceAlternative.Value)
                 : fileName;
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
