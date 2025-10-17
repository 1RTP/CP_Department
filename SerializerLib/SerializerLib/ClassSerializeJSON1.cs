using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SerializerLib
{
    public static class ClassSerializeJSON1
    {
        public static void SerializeToJSON<T>(T obj, string filePath, Action<string> log = null)
        {
            try
            {
                string jsonString = JsonConvert.SerializeObject(obj, Formatting.Indented);
                File.WriteAllText(filePath, jsonString, Encoding.UTF8);
                log?.Invoke($"Збережено об’єкт {typeof(T).Name} у {filePath}");
            }
            catch (Exception ex)
            {
                log?.Invoke($"Помилка при серіалізації (JSON): {ex.Message}");
                throw;
            }
        }

        public static T DeserializeFromJSON<T>(string filePath, Action<string> log = null)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    log?.Invoke($"Файл {filePath} не знайдено. Повертається дефолт типу {typeof(T).Name}");
                    return default(T);
                }

                string jsonString = File.ReadAllText(filePath, Encoding.UTF8);
                var obj = JsonConvert.DeserializeObject<T>(jsonString);
                log?.Invoke($"Завантажено об’єкт {typeof(T).Name} з {filePath}");
                return obj;
            }
            catch (Exception ex)
            {
                log?.Invoke($"Помилка при десеріалізації (JSON): {ex.Message}");
                return default(T);
            }
        }
    }
}
