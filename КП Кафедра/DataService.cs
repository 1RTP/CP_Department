using NLog;
using System;
using System.Collections.Generic;
using System.Data;
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
        public static List<Participation> Participations = new List<Participation>();

    }
}