using IconLib.Models;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace IconLib.Works
{
    public abstract class ImgWork
    {
        [JsonIgnore]
        protected ImageFileInfo ImageFile { get; set; }

        [JsonIgnore]
        protected string TargetFile { get; set; }

        public string TargetExtension
        {
            get
            {
                if (string.IsNullOrEmpty(_targetExtension) && ImageFile == null)
                    return string.Empty;

                if (string.IsNullOrEmpty(_targetExtension))
                    return ImageFile.Extension;

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



        public void Execute(string targetFile, ImageFileInfo imageFile)
        {
            TargetFile = targetFile;
            ImageFile = imageFile;
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
