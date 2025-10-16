using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NLog;

namespace SerializerLib
{
    internal class SerializerJSON
    {
        public static void SerializeToJSON<T>(List<T> data, string filePath, Action<string> log = null)
        {
            try
            {
                string jsonString = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(filePath, jsonString, Encoding.UTF8);
                log?.Invoke($"Збережено {data.Count} об’єктів у {filePath}");
            }
            catch (Exception ex) { log?.Invoke($"Помилка при серіалізації (JSON): {ex.Message}"); throw; }
        }

        public static List<T> DeserializeFromJSON<T>(string filePath, Action<string> log = null)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    log?.Invoke($"Файл {filePath} не знайдено. Повертається порожній список.");
                    return new List<T>();
                }

                string jsonString = File.ReadAllText(filePath, Encoding.UTF8);
                var data = JsonConvert.DeserializeObject<List<T>>(jsonString) ?? new List<T>();
                log?.Invoke($"Завантажено {data.Count} об’єктів з {filePath}");
                return data;
            }
            catch (Exception ex)
            {
                log?.Invoke($"Помилка при десеріалізації (JSON): {ex.Message}");
                return new List<T>();
            }
        }
    }
}
