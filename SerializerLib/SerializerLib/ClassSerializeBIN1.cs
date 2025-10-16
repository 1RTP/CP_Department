using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SerializerLib
{
    internal class ClassSerializeBIN
    {
        public static void SerializeToBIN<T>(List<T> data, string filePath, Action<string> log)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, data);
                }
                log?.Invoke($"Збережено {data.Count} об’єктів у {filePath}");
            }
            catch (Exception ex) { log?.Invoke($"Помилка при серіалізації (BIN): {ex.Message}"); throw; }
        }

        public static List<T> DeserializeFromBIN<T>(string filePath, Action<string> log)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    log?.Invoke($"Файл {filePath} не знайдено, десеріалізація не виконана.");
                    return new List<T>();
                }

                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.Length == 0)
                {
                    log?.Invoke($"Файл {filePath} порожній.");
                    return new List<T>();
                }

                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    var data = (List<T>)formatter.Deserialize(fs);
                    log?.Invoke($"Завантажено {data.Count} об’єктів з {filePath}");
                    return data;
                }
            }
            catch (Exception ex)
            {
                log?.Invoke($"Помилка при десеріалізації (BIN): {ex.Message}");
                return new List<T>();
            }
        }
    }
}
