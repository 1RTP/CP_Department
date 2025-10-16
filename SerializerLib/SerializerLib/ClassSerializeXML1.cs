using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SerializerLib
{
    internal class ClassSerializeXML
    {
        public static void SerializeToXml<T>(T obj, string filePath, Action<string> log)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                var ns = new XmlSerializerNamespaces();
                ns.Add("", ""); // видаляє стандартні простори імен (xmlns:xsi, xmlns:xsd)
                using (var stream = new StreamWriter(filePath, false, new UTF8Encoding(false)))
                {
                    serializer.Serialize(stream, obj, ns);
                }
                log?.Invoke($"Збережено об’єкт у {filePath}");
            }
            catch (Exception ex) { log?.Invoke($"Помилка при серіалізації (XML): {ex.Message}"); throw; }
        }

        public static T DeserializeFromXml<T>(string filePath, Action<string> log)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (var reader = new StreamReader(filePath, Encoding.UTF8))
                {
                    var result = (T)serializer.Deserialize(reader);
                    log?.Invoke($"Завантажено об’єкт з {filePath}");
                    return result;
                }
            }
            catch (Exception ex)
            {
                log?.Invoke($"Помилка при десеріалізації (XML): {ex.Message}");
                return default(T);
            }
        }
    }
}
