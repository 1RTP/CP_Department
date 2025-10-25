using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace КП_Кафедра
{
    [Serializable]
    public class DepartmentData
    {
        public List<Teacher> Teachers { get; set; } = new List<Teacher>();
        public List<Subject> Subjects { get; set; } = new List<Subject>();
        public List<Assignment> Assignments { get; set; } = new List<Assignment>();
        public List<LessonType> LessonTypes { get; set; } = new List<LessonType>();
        public List<Research> Researches { get; set; } = new List<Research>();
        public List<Participation> Participations { get; set; } = new List<Participation>();

        public DepartmentData() { }
    }
}
