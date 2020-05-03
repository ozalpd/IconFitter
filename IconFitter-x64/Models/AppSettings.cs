using Newtonsoft.Json;
using System;
using System.IO;

namespace IconFitter.Models
{
    public class AppSettings
    {
        public WindowPosition MainWindowPosition
        {
            set { _mainWindowPosition = value; }
            get
            {
                if (_mainWindowPosition == null)
                {
                    _mainWindowPosition = new WindowPosition();
                }
                return _mainWindowPosition;
            }
        }
        WindowPosition _mainWindowPosition;


        [JsonIgnore]
        public string RecentFileName { get; protected set; }

        public static AppSettings OpenFromFile(string fileName)
        {
            return OpenFromFile(fileName, TypeNameHandling.Objects);
        }

        public static AppSettings OpenFromFile(string fileName, TypeNameHandling typeNameHandling)
        {
            using (var reader = new StreamReader(fileName))
            {
                var serializer = new JsonSerializer();
                serializer.TypeNameHandling = typeNameHandling;
                var instance = serializer.Deserialize(reader, typeof(AppSettings)) as AppSettings;
                reader.Close();

                instance.RecentFileName = fileName;

                return instance;
            }
        }

        public void SaveToFile()
        {
            if (string.IsNullOrEmpty(RecentFileName))
                throw new Exception("No RecentFileName has been set!");

            SaveToFile(RecentFileName);
        }

        public void SaveToFile(string fileName, bool createMissingDirectory = true)
        {
            SaveToFile(fileName, createMissingDirectory, TypeNameHandling.Objects);
        }

        public virtual void SaveToFile(string fileName, bool createMissingDirectory, TypeNameHandling typeNameHandling)
        {
            var folder = Path.GetDirectoryName(fileName);
            if (createMissingDirectory && !Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                var serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.NullValueHandling = NullValueHandling.Include;
                serializer.TypeNameHandling = typeNameHandling;

                serializer.Serialize(writer, this, this.GetType());
                writer.Close();
            }
        }
    }
}
