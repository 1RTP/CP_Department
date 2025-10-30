using NLog;
using SerializerLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using КП_Кафедра.Forms;
using КП_Кафедра.Properties;
using static КП_Кафедра.ToastForm;

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

        public Subject() { }

        public Subject(int id = 0, string name = "", int sem = 0, int hours = 0, bool st = true) 
        { 
            SubjectId = id; 
            Name = name; 
            Semester = sem; 
            TotalHours = hours; 
            Status = st; 
        }

    }

    [Serializable]
    public class LessonType
    {
        public int LessonTypeId { get; set; }
        public string TypeName { get; set; }

        public LessonType() { }
        public LessonType(int id = 0, string typeName = "") { LessonTypeId = id; TypeName = typeName; }

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

        public Teacher() { }
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

    }

    [Serializable]
    public class Assignment
    {
        public int AssignmentId { get; set; }
        public int PlanHours { get; set; }
        public int TaughtHours { get; set; }
        public DateTime HireDate { get; set; }
        public bool Status { get; set; }

        [XmlIgnore]
        public List<Teacher> Teachers { get; set; } 

        public Assignment() { }

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
    }

    [Serializable]
    public class Participation
    {
        public int ParticipationId { get; set; }
        public Research Project { get; set; }

        [XmlIgnore]
        public Teacher Teacher { get; set; }
        public Participation() { }

        public Participation(int id = 0, Teacher t = null, Research r = null) { ParticipationId = id; Project = r; Teacher = t; }
    }

    [Serializable]
    public class Research
    {
        public int ResearchId { get; set; }
        public string ResearchName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Participation> Participants { get; set; }

        public Research() { }
        public Research(int id = 0, string name = "", DateTime? start = null, DateTime? end = null)
        {
            ResearchId = id;
            ResearchName = name;
            StartDate = start ?? DateTime.MinValue;
            EndDate = end ?? DateTime.MinValue;
            Participants = new List<Participation>();
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

                //string dbPath = "Data/department.db";
                //string sqlInitFile = "Data/init_data.sql";

                string exeDir = AppDomain.CurrentDomain.BaseDirectory;
                string projectRoot = Path.GetFullPath(Path.Combine(exeDir, "..", ".."));
                string dbPath = Path.Combine(projectRoot, "Data", "department.db");
                string sqlInitFile = Path.Combine(projectRoot, "Data", "init_data.sql");

                DatabaseSQL.Initialize(dbPath, sqlInitFile);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormMainMenu("Name", "admin@gmail.com"));
                //Application.Run(new FormLogin());
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
