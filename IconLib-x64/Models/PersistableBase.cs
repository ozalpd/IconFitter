using Newtonsoft.Json;
using System;
using System.IO;

namespace IconLib.Models
{
    public abstract class PersistableBase
    {
        [JsonIgnore]
        public string RecentFileName { get; protected set; }


        //Method GetInstanceFromFile can not resolve in derived classes from another assemblies
        protected static PersistableBase GetInstanceFromFile(string fileName, Type type, TypeNameHandling typeNameHandling = TypeNameHandling.Objects)
        {
            if (!typeof(PersistableBase).IsAssignableFrom(type))
            {
                throw new Exception("The type must be inherited from PersistableBase");
            }
            using (var reader = new StreamReader(fileName))
            {
                var serializer = new JsonSerializer();
                serializer.TypeNameHandling = typeNameHandling;
                var instance = serializer.Deserialize(reader, type) as PersistableBase;
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
