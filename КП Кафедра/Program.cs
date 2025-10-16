using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static КП_Кафедра.ToastForm;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.Threading;
using КП_Кафедра.Properties;
using SerializerLib;

namespace КП_Кафедра
{
    [Serializable]
    public class Subject
    {
        public int SubjectId { get; set; }
        public string Name { get; set; }
        public int Semester { get; set; }
        public int TotalHours { get; set; }
        public bool Status { get; set; }

        public Subject(int id = 0, string name = "", int sem = 0, int hours = 0, bool st = true) 
        { 
            SubjectId = id; 
            Name = name; 
            Semester = sem; 
            TotalHours = hours; 
            Status = st; 
        }

        public string GetInfo()
        {
            return $"Subject: {Name}, Semester: {Semester}, Hours: {TotalHours}, Active: {Status}";
        }
    }

    [Serializable]
    public class LessonType
    {
        public int LessonTypeId { get; set; }
        public string TypeName { get; set; }

        public LessonType(int id = 0, string typeName = "") { LessonTypeId = id; TypeName = typeName; }

        public string GetInfo()
        {
            return $"Lesson Type: {TypeName}";
        }
    }

    [Serializable]
    public class Teacher
    {
        public int EmpId { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public DateTime HireDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }

        public List<Participation> Participations { get; set; }
        public List<Assignment> Assignments { get; set; }

        public Teacher(int id = 0, string name = "") 
        { 
            EmpId = id; 
            FullName = name; 
            Position = ""; 
            HireDate = DateTime.MinValue; // DateTime.MinValue - дата ще не задана, DateTime.Now - поточна дата 
            PhoneNumber = ""; 
            Email = "";
            Status = true; 
            Participations = new List<Participation>();
            Assignments = new List<Assignment>(); 
        }

        public string GetShortName() // скорочене ім’я
        {
            string[] parts = FullName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 3) return $"{parts[0]} {parts[1][0]}. {parts[2][0]}.";
            else if (parts.Length == 2) return $"{parts[0]} {parts[1][0]}.";
            return FullName;
        }

        public Assignment AssignSubject(Subject subject, LessonType lessonType, int planHours, int assignmentId) // призначення предмета викладачу
        {
            Assignment newAssignment = new Assignment(assignmentId, planHours)
            {
                LessonType = lessonType
            };
            newAssignment.Subjects.Add(subject);
            newAssignment.Teachers.Add(this);
            Assignments.Add(newAssignment);
            return newAssignment;
        }

        public Participation JoinResearch(Research r, int participationId) // участь у НДР
        {
            Participation p = new Participation(participationId, this, r);
            Participations.Add(p);
            r.Participants.Add(p);
            return p;
        }

        public string GetInfo()
        {
            string hireDateText = HireDate == DateTime.MinValue ? "Not set" : HireDate.ToString("dd.MM.yyyy");
            return $"Teacher: {FullName}, Position: {Position}, Hire Date: {hireDateText}, Phone: {PhoneNumber}, Email: {Email}, Active: {Status}";
        }
    }

    [Serializable]
    public class Assignment
    {
        public int AssignmentId { get; set; }
        public int PlanHours { get; set; }
        public int TaughtHours { get; set; }
        public DateTime HireDate { get; set; }
        public bool Status { get; set; }

        [NonSerialized]
        //public List<Teacher> Teachers { get; set; }
        public List<Teacher> Teachers;
        public List<Subject> Subjects { get; set; }
        public LessonType LessonType { get; set; }

        public Assignment(int id = 0, int plan = 0) 
        { 
            AssignmentId = id; 
            PlanHours = plan; 
            TaughtHours = 0;
            HireDate = DateTime.MinValue;
            Status = true; 
            Teachers = new List<Teacher>(); 
            Subjects = new List<Subject>(); 
            LessonType = null; 
        }

        public void UpdateHours(int taught) // оновлення відпрацьованих годин
        {
            TaughtHours += taught;
            if (TaughtHours > PlanHours) TaughtHours = PlanHours;
        }

        public string GetInfo()
        {
            string hireDateText = HireDate == DateTime.MinValue ? "Not set" : HireDate.ToString("dd.MM.yyyy");
            return $"Assignment {AssignmentId}: Plan {PlanHours}, Done {TaughtHours}, Hire Date: {hireDateText}, Lesson: {LessonType?.TypeName}";
        }
    }

    [Serializable]
    public class Participation
    {
        public int ParticipationId { get; set; }
        public Research Project { get; set; }

        [NonSerialized]
        //public Teacher Teacher { get; set; }
        public Teacher Teacher;

        public Participation(int id = 0, Teacher t = null, Research r = null) { ParticipationId = id; Project = r; Teacher = t; }

        public string ParticipationCheck(Teacher t, Research r) // перевірка участі
        { 
            if (t != null && r != null) return $"{t.FullName} participates in {r.ResearchName}";
            return "Invalid participation data";
        }

        public string GetInfo()
        {
            return $"Participation {ParticipationId}: {Teacher?.FullName} in {Project?.ResearchName}";
        }
    }

    [Serializable]
    public class Research
    {
        public int ResearchId { get; set; }
        public string ResearchName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Participation> Participants { get; set; }

        public Research(int id = 0, string name = "", DateTime? start = null, DateTime? end = null)
        {
            ResearchId = id;
            ResearchName = name;
            StartDate = start ?? DateTime.MinValue;
            EndDate = end ?? DateTime.MinValue;
            Participants = new List<Participation>();
        }

        public Participation AddParticipant(Teacher t, int participationId) // додавання викладача до дослідження
        {
            Participation p = new Participation(participationId, t, this);
            Participants.Add(p);
            t.Participations.Add(p);
            return p;
        }

        public int GetDuration() // тривалість дослідження в днях
        {
            return (EndDate - StartDate).Days;
        }

        public string GetInfo()
        {
            string startDateText = StartDate == DateTime.MinValue ? "Not set" : StartDate.ToString("dd.MM.yyyy");
            string endDateText = EndDate == DateTime.MinValue ? "Not set" : EndDate.ToString("dd.MM.yyyy");
            return $"Research: {ResearchName}, Start: {startDateText}, End: {endDateText}, Participants: {Participants.Count}";
        }
    }

    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            LoggerService.LogInfo("Програма стартувала");

            try
            {
                string lang = Settings.Default.Language;
                string culture = lang == "English" ? "en" : "uk";
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);

                string dbPath = "Data/department.db";
                string sqlInitFile = "Data/init_data.sql";
                DatabaseSQL.Initialize(dbPath, sqlInitFile);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormMainMenu()); 
            }
            catch (Exception ex)
            {
                LoggerService.LogError($"Помилка під час запуску програми: {ex.Message}");
                MessageBox.Show($"Виникла помилка: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                LoggerService.LogInfo("Програма завершена");
            }
        }
    }
}
