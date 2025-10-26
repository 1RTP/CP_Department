using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using Microsoft.Data.Sqlite;

namespace КП_Кафедра
{
    internal static class DatabaseSQL
    {
        public static void Initialize(string dbPath, string sqlInitFile)
        {
            if (!File.Exists(dbPath))
            {
                LoggerService.LogInfo("База даних не знайдена. Створено нову базу даних");
                using (File.Create(dbPath)) { }
            }

            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                string createTables = @"
                    CREATE TABLE IF NOT EXISTS teacher (
                        emp_id INTEGER PRIMARY KEY AUTOINCREMENT,
                        emp_full_name TEXT,
                        emp_position TEXT,
                        emp_hire_date TEXT,
                        phone_number TEXT,
                        email TEXT,
                        status INTEGER DEFAULT 1
                    );

                    CREATE TABLE IF NOT EXISTS subjects (
                        subject_id INTEGER PRIMARY KEY AUTOINCREMENT,
                        subject_name TEXT,
                        semester INTEGER,
                        total_hours INTEGER
                    );

                    CREATE TABLE IF NOT EXISTS lesson_type (
                        lesson_type_id INTEGER PRIMARY KEY AUTOINCREMENT,
                        type_name TEXT
                    );

                    CREATE TABLE IF NOT EXISTS assignment (
                        assignment_id INTEGER PRIMARY KEY AUTOINCREMENT,
                        emp_id INTEGER,
                        subject_id INTEGER,
                        lesson_type_id INTEGER,
                        plan_hours INTEGER,
                        hours_taught INTEGER
                    );

                    CREATE TABLE IF NOT EXISTS research (
                        research_id INTEGER PRIMARY KEY AUTOINCREMENT,
                        research_name TEXT,
                        start_date TEXT,
                        end_date TEXT
                    );

                    CREATE TABLE IF NOT EXISTS participation_in_research (
                        participation_id INTEGER PRIMARY KEY AUTOINCREMENT,
                        emp_id INTEGER,
                        research_id INTEGER
                    );
                ";

                using (var cmd = new SqliteCommand(createTables, connection)) cmd.ExecuteNonQuery();

                string checkColumn = "PRAGMA table_info(teacher);";
                bool hasStatus = false;
                using (var cmd = new SqliteCommand(checkColumn, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["name"].ToString() == "status")
                        {
                            hasStatus = true;
                            break;
                        }
                    }
                }

                if (!hasStatus)
                {
                    using (var cmd = new SqliteCommand("ALTER TABLE teacher ADD COLUMN status INTEGER DEFAULT 1;", connection))
                    {
                        cmd.ExecuteNonQuery();
                        LoggerService.LogInfo("Колонку status додано до таблиці teacher.");
                    }
                }


                string checkData = "SELECT COUNT(*) FROM teacher;";
                using (var cmd = new SqliteCommand(checkData, connection))
                {
                    long count = (long)cmd.ExecuteScalar();
                    if (count == 0)
                    {
                        string insertData = File.ReadAllText(sqlInitFile);
                        using (var transaction = connection.BeginTransaction())
                        using (var insertCmd = new SqliteCommand(insertData, connection, transaction))
                        {
                            insertCmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        LoggerService.LogInfo("Базу даних створено та заповнено початковими даними!");
                    }
                    else
                    {
                        LoggerService.LogInfo($"Базу даних знайдено. Ініціалізація пропущена.");
                    }
                }
            }
        }
    }
}
