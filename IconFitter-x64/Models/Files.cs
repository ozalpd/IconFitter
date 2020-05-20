using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IconFitter.Models
{
    public enum FileType
    {
        ImageFile = 10,
        ResizeWorkSet = 20
    }

    public static class Files
    {
        public static string GetExtension(FileType fileType)
        {
            switch (fileType)
            {
                case FileType.ImageFile:
                    return ".png";

                case FileType.ResizeWorkSet:
                    return ".resize";

                default:
                    return string.Empty;
            }
        }

        public static string GetFilter(FileType fileType)
        {
            switch (fileType)
            {
                case FileType.ImageFile:
                    return imgFilter;

                case FileType.ResizeWorkSet:
                    return "Resize WorkSet files|*.resize";

                default:
                    return "All types of files|*.*";
            }
        }

        static string imgFilter = "All supported graphics|*.jpg;*.jpeg;*.png;*.psd|" +
                                  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                                  "Portable Network Graphic (*.png)|*.png";
    }
}
