using Newtonsoft.Json;

namespace IconLib.Works
{
    public class ResizeWorkSet : ImgWorkSet<ResizeWork>
    {
        public static ResizeWorkSet OpenFromFile(string fileName)
        {
            return (ResizeWorkSet)GetInstanceFromFile(fileName, typeof(ResizeWorkSet), TypeNameHandling.None);
        }
    }
}
