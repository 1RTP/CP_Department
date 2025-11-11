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

    //public sealed class LoggerSingleton // sealed - не дозволяє наслідування
    //{
    //    private static LoggerSingleton _instance;
    //    private static readonly object _lock = new object();
    //    private readonly Logger _logger;

    //    private LoggerSingleton() { _logger = LogManager.GetCurrentClassLogger(); } 

    //    public static LoggerSingleton Instance
    //    {
    //        get
    //        {
    //            if (_instance == null) // захист від багатопотокового доступу
    //            {
    //                lock (_lock) if (_instance == null) _instance = new LoggerSingleton();
    //            }
    //            return _instance;
    //        }
    //    }

    //    public void LogInfo(string message) => _logger.Info(message);
    //    public void LogWarn(string message) => _logger.Warn(message);
    //    public void LogError(string message) => _logger.Error(message);
    //}

    public static class DataService
    {
        public static List<Teacher> Teachers = new List<Teacher>();
        public static List<Subject> Subjects = new List<Subject>();
        public static List<Research> Researches = new List<Research>();
        public static List<Assignment> Assignments = new List<Assignment>();
        public static List<Participation> Participations = new List<Participation>();

    }
}