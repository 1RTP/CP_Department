using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace КП_Кафедра
{
    internal static class ClassSerializeJSON
    {
        //public static void SerializeToJSON<T>(List<T> data, string filePath)
        //{
        //    try
        //    {
        //        string jsonString = JsonConvert.SerializeObject(data, Formatting.Indented);
        //        File.WriteAllText(filePath, jsonString, Encoding.UTF8);
        //        LoggerService.LogInfo($"Збережено {data.Count} об’єктів у {filePath}");
        //    }
        //    catch (Exception ex) { LoggerService.LogError($"Помилка при серіалізації (JSON): {ex.Message}"); throw; }
        //}

        //public static List<T> DeserializeFromJSON<T>(string filePath)
        //{
        //    try
        //    {
        //        if (!File.Exists(filePath)) { LoggerService.LogInfo($"Файл {filePath} не знайдено. Повертається порожній список."); return new List<T>(); }
        //        string jsonString = File.ReadAllText(filePath, Encoding.UTF8);
        //        var data = JsonConvert.DeserializeObject<List<T>>(jsonString) ?? new List<T>();
        //        LoggerService.LogInfo($"Завантажено {data.Count} об’єктів з {filePath}");
        //        return data;
        //    }
        //    catch (Exception ex) { LoggerService.LogError($"Помилка при десеріалізації (JSON): {ex.Message}"); return new List<T>(); }
        //}
    }
}
