using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SerializerLib
{
    public static class ClassSerializeBIN1
    {
        public static void SerializeToBIN<T>(T obj, string filePath, Action<string> log = null)
        {
            try
            {
                using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(fs, obj);
                }
                log?.Invoke($"Збережено об’єкт {typeof(T).Name} у {filePath}");
            }
            catch (Exception ex)
            {
                log?.Invoke($"Помилка при серіалізації (BIN): {ex.Message}");
                throw;
            }
        }

        public static T DeserializeFromBIN<T>(string filePath, Action<string> log = null)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    log?.Invoke($"Файл {filePath} не знайдено. Повертається дефолт типу {typeof(T).Name}");
                    return default(T);
                }

                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    var formatter = new BinaryFormatter();
                    var obj = (T)formatter.Deserialize(fs);
                    log?.Invoke($"Завантажено об’єкт {typeof(T).Name} з {filePath}");
                    return obj;
                }
            }
            catch (Exception ex)
            {
                log?.Invoke($"Помилка при десеріалізації (BIN): {ex.Message}");
                return default(T);
            }
        }
    }
}
