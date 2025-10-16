using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using SerializerLib;


namespace КП_Кафедра
{

//    internal static class ClassSerializeBIN
//    {
//        public static void SerializeToBIN<T>(List<T> data, string filePath)
//        {
//            try
//            {
//                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
//                {
//#pragma warning disable SYSLIB0011
//                    BinaryFormatter formatter = new BinaryFormatter();
//                    formatter.Serialize(fs, data);
//#pragma warning restore SYSLIB0011
//                }
//                LoggerService.LogInfo($"Збережено {data.Count} об’єктів у {filePath}");
//            }
//            catch (Exception ex) { LoggerService.LogError($"Помилка при серіалізації (BIN): {ex.Message}"); throw; }
//        }

//        public static List<T> DeserializeFromBIN<T>(string filePath)
//        {
//            try
//            {
//                if (!File.Exists(filePath))
//                {
//                    LoggerService.LogError($"Файл {filePath} не знайдено, десеріалізація не виконана.");
//                    return new List<T>();
//                }

//                FileInfo fileInfo = new FileInfo(filePath);
//                if (fileInfo.Length == 0)
//                {
//                    LoggerService.LogError($"Файл {filePath} порожній.");
//                    return new List<T>();
//                }

//                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
//                {
//#pragma warning disable SYSLIB0011
//                    BinaryFormatter formatter = new BinaryFormatter();
//                    var data = (List<T>)formatter.Deserialize(fs);
//#pragma warning restore SYSLIB0011
//                    LoggerService.LogInfo($"Завантажено {data.Count} об’єктів з {filePath}");
//                    return data;
//                }
//            }
//            catch (Exception ex) { LoggerService.LogError($"Помилка при десеріалізації (BIN): {ex.Message}"); return new List<T>(); }
//        }
//    }


}
