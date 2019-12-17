using Polly;
using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace XMLTestApp.Extensions
{
    internal static class XMLExtensions
    {
        internal static void SaveAsXML<T>(this T serializableObject, string path)
        {
            if (IsValidObject(serializableObject))
            {
                WriteXMLToFile(serializableObject, path);
            }
        }

        internal static T LoadFromXML<T>(this string path)
        {
            T deserializedObject = default(T);
            if (IsValidXMLPath(path))
            {
                deserializedObject = ReadXMLToObject<T>(path);
            }
            return deserializedObject;
        }

        private static bool IsValidObject<T>(T serializableObject)
        {
            if (serializableObject.Equals(null))
            {
                throw new ArgumentNullException($"{serializableObject.GetType().FullName} cannot be serialized because it has not value");
            }
            return true;
        }

        private static void WriteXMLToFile<T>(T serializableObject, string path)
        {
            StreamWriter outputWriteStream = default(StreamWriter);
            FileStream outputFileStream = default(FileStream);
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                outputFileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                outputWriteStream = new StreamWriter(outputFileStream, Encoding.UTF8);
                xmlSerializer.Serialize(outputWriteStream, serializableObject);
            }
            finally
            {
                outputWriteStream?.Dispose();
                outputFileStream?.Dispose();
            }
        }

        private static bool IsValidXMLPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("Path argument cannot be null or empty");
            }
            else if (!File.Exists(path))
            {
                throw new Exception($"Unable to access to \"{path}\" path");
            }
            return true;
        }

        private static T ReadXMLToObject<T>(string path)
        {
            T deserializedObject = default(T);
            FileStream inputFileStream = default(FileStream);
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                inputFileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                Policy.Handle<InvalidOperationException>()
                        .RetryForever()
                        .Execute(() =>
                        {
                            deserializedObject = (T)xmlSerializer.Deserialize(inputFileStream);
                        });
            }
            finally
            {
                inputFileStream?.Dispose();
            }
            return deserializedObject;
        }
    }
}
