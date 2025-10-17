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
}
