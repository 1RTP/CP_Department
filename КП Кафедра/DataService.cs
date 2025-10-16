using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace КП_Кафедра
{
    public static class LoggerService
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public static void LogInfo(string message) { logger.Info(message); }
        public static void LogWarn(string message) { logger.Warn(message); }
        public static void LogError(string message) { logger.Error(message); }
    }

    public static class DataService
    {
        public static List<Teacher> Teachers = new List<Teacher>();
        public static List<Subject> Subjects = new List<Subject>();
        public static List<Research> Researches = new List<Research>();
        public static List<Assignment> Assignments = new List<Assignment>();

        public static bool AddTeacher(Teacher t)
        {
            if (Teachers.Any(x => x.PhoneNumber == t.PhoneNumber))
            {
                LoggerService.LogWarn($"Викладач з ID {t.PhoneNumber} вже існує");
                return false;
            }
            Teachers.Add(t);
            LoggerService.LogInfo($"Додано викладача: {t.FullName}");
            return true;
        }

        public static bool DelTeacher(string fullName)
        {
            var t = Teachers.FirstOrDefault(x => x.FullName == fullName);
            if (t != null)
            {
                t.Status = false;
                LoggerService.LogInfo($"Деактивовано викладача: {fullName}");
                return true;
            }
            return false;
        }

        public static void AddSubject(Subject s)
        {
            Subjects.Add(s);
            LoggerService.LogInfo($"Додано дисципліну: {s.Name}");
        }

        public static bool UpdateSubject(string name, int semester, int totalHours)
        {
            var s = Subjects.FirstOrDefault(x => x.Name == name);
            if (s != null)
            {
                s.Semester = semester;
                s.TotalHours = totalHours;
                LoggerService.LogInfo($"Оновлено дисципліну: {name}");
                return true;
            }
            return false;
        }

        public static bool DelSubject(string name)
        {
            var s = Subjects.FirstOrDefault(x => x.Name == name);
            if (s != null)
            {
                s.Status = false;
                LoggerService.LogInfo($"Деактивовано дисципліну: {name}");
                return true;
            }
            return false;
        }

        public static void AddResearch(Research r)
        {
            Researches.Add(r);
            LoggerService.LogInfo($"Додано НДР: {r.ResearchName}");
        }

        public static bool UpdateResearch(string name, DateTime start, DateTime end)
        {
            var r = Researches.FirstOrDefault(x => x.ResearchName == name);
            if (r != null)
            {
                r.StartDate = start;
                r.EndDate = end;
                LoggerService.LogInfo($"Оновлено НДР: {name}");
                return true;
            }
            return false;
        }

        public static bool DelResearch(string name)
        {
            var r = Researches.FirstOrDefault(x => x.ResearchName == name);
            if (r != null)
            {
                foreach (var p in r.Participants.ToList()) p.Teacher.Participations.Remove(p);
                Researches.Remove(r);
                LoggerService.LogInfo($"Видалено НДР: {name}");
                return true;
            }
            return false;
        }

        public static Assignment AddAssignment(Teacher t, Subject s, LessonType l, int planHours)
        {
            var a = new Assignment { PlanHours = planHours };
            a.Subjects.Add(s);
            a.Teachers.Add(t);
            a.LessonType = l;
            Assignments.Add(a);
            t.Assignments.Add(a);
            LoggerService.LogInfo($"Додано призначення для {t.FullName} з дисципліни {s.Name}");
            return a;
        }

        public static bool UpdateAssignment(int id, int planHours, int taughtHours)
        {
            var a = Assignments.FirstOrDefault(x => x.AssignmentId == id);
            if (a != null)
            {
                a.PlanHours = planHours;
                a.TaughtHours = taughtHours;
                LoggerService.LogInfo($"Оновлено призначення ID = {id}");
                return true;
            }
            return false;
        }

        public static bool DeleteAssignment(int id)
        {
            var a = Assignments.FirstOrDefault(x => x.AssignmentId == id);
            if (a != null)
            {
                foreach (var t in a.Teachers) t.Assignments.Remove(a);
                Assignments.Remove(a);
                LoggerService.LogInfo($"Видалено призначення ID = {id}");
                return true;
            }
            return false;
        }
    }
}