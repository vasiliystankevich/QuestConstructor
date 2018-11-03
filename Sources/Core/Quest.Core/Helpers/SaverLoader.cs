﻿using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Quest.Core.Helpers
{
    public class SaverLoader
    {
        public static void Save<T>(T obj, string filePath)
        {
            using (var fs = File.OpenWrite(filePath))
                new BinaryFormatter().Serialize(fs, obj);
        }

        public static T Load<T>(string filePath)
        {
            using (var fs = File.OpenRead(filePath))
                return (T)new BinaryFormatter().Deserialize(fs);
        }
    }
}
