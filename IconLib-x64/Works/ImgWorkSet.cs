using IconLib.Models;
using System;
using System.IO;

namespace IconLib.Works
{
    public class ImgWorkSet<T> : PersistableList<T> where T : ImgWork
    {
        /// <summary>A string representing the target directory's full path.</summary>
        public string TargetDirectory { get; set; }

        public virtual void Execute(ImageFileInfo sourceImage)
        {
            foreach (var work in this)
            {
                work.Execute(sourceImage, GetTargetDirectory());
            }
        }


        /// <summary>
        /// Returns absolute path of TargetDirectory. If it is null or empty returns a default one
        /// </summary>
        protected virtual string GetTargetDirectory()
        {
            return string.IsNullOrWhiteSpace(TargetDirectory)
                 ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "IconFitter")
                 : Path.IsPathRooted(TargetDirectory)
                 ? Path.GetFullPath(TargetDirectory)
                 //: Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), TargetDirectory);
                 : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), TargetDirectory);
        }
    }
}
