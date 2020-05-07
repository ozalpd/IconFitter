using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace IconLib.Models
{
    public class PersistableList<T> : List<T>
    {
        [JsonIgnore]
        public string RecentFileName { get; protected set; }


        protected static PersistableList<T> GetInstanceFromFile(string fileName, Type type, TypeNameHandling typeNameHandling = TypeNameHandling.Objects)
        {
            if (!typeof(PersistableList<T>).IsAssignableFrom(type))
            {
                throw new Exception("The type must be inherited from PersistableList");
            }
            using (var reader = new StreamReader(fileName))
            {
                var serializer = new JsonSerializer();
                serializer.TypeNameHandling = typeNameHandling;
                var instance = serializer.Deserialize(reader, type) as PersistableList<T>;
                reader.Close();

                return instance;
            }
        }


        public void SaveToFile()
        {
            if (string.IsNullOrEmpty(RecentFileName))
                throw new Exception("No RecentFileName is set!");

            SaveToFile(RecentFileName);
        }

        public virtual void SaveToFile(string fileName, bool createMissingDirectory = true, TypeNameHandling typeNameHandling = TypeNameHandling.Objects)
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
