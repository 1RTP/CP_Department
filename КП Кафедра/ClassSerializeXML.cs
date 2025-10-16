using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using SerializerLib;

namespace КП_Кафедра
{

    [XmlType("Teacher")]
    public class TeacherDto // DTO (Data Transfer Object) — об’єкт, що використовується для передачі даних з одного місця програми в інше.
    {
        [XmlAttribute] public int EmpId { get; set; }
        [XmlElement] public string FullName { get; set; }
        [XmlElement] public string Position { get; set; }
        [XmlElement(DataType = "date")] public DateTime HireDate { get; set; }
        [XmlElement] public string PhoneNumber { get; set; }
        [XmlElement] public string Email { get; set; }
        [XmlElement] public bool Status { get; set; }
        public TeacherDto() { }

        public static TeacherDto FromTeacher(Teacher t)
        {
            return new TeacherDto
            {
                EmpId = t.EmpId,
                FullName = t.FullName,
                Position = t.Position,
                HireDate = t.HireDate,
                PhoneNumber = t.PhoneNumber,
                Email = t.Email,
                Status = t.Status
            };
        }

        public Teacher ToTeacher()
        {
            var t = new Teacher(EmpId, FullName)
            {
                Position = Position,
                HireDate = HireDate,
                PhoneNumber = PhoneNumber,
                Email = Email,
                Status = Status
            };
            return t;
        }
    }

    public static class XmlHelper
    {
        public static void SerializeTeachers(List<Teacher> teachers, string filePath)
        {
            try
            {
                var dtoList = teachers.Select(TeacherDto.FromTeacher).ToList();
                ClassSerializeXML1.SerializeToXml(dtoList, filePath);
                LoggerService.LogInfo($"Збережено {dtoList.Count} викладачів у {filePath}");
            }
            catch (Exception ex) { LoggerService.LogError($"Помилка при серіалізації викладачів: {ex.Message}"); throw; }
        }

        public static List<Teacher> DeserializeTeachers(string filePath)
        {
            try
            {
                var dtoList = ClassSerializeXML1.DeserializeFromXml<List<TeacherDto>>(filePath);
                var teachers = dtoList.Select(d => d.ToTeacher()).ToList();
                LoggerService.LogInfo($"Завантажено {teachers.Count} викладачів з {filePath}");
                return teachers;
            }
            catch (Exception ex) { LoggerService.LogError($"Помилка при десеріалізації викладачів: {ex.Message}"); return new List<Teacher>(); }
        }
    }
}
