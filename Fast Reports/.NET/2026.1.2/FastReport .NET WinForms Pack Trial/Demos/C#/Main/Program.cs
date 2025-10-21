using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;
using FastReport.Data;

namespace Demo
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
#if NETFRAMEWORK
        ConfigurationManager.AppSettings.Set("EnableWindowsFormsHighDpiAutoResizing", "true"); 
#endif
        InitDB();

        Application.EnableVisualStyles(); 
        Application.SetCompatibleTextRenderingDefault(false); 
        Application.Run(new Form1());
    }

    private static void InitDB()
    {
#if RELEASEINCLUDEPLUGINS
        FastReport.Utils.RegisteredObjects.AddConnection(typeof(PostgresDataConnection));
        FastReport.Utils.RegisteredObjects.AddConnection(typeof(MySqlDataConnection));
        FastReport.Utils.RegisteredObjects.AddConnection(typeof(FirebirdDataConnection));
        FastReport.Utils.RegisteredObjects.AddConnection(typeof(MongoDBDataConnection));
        FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
        FastReport.Utils.RegisteredObjects.AddConnection(typeof(SQLiteDataConnection));
        FastReport.Utils.RegisteredObjects.AddConnection(typeof(CouchbaseDataConnection));
        FastReport.Utils.RegisteredObjects.AddConnection(typeof(RavenDBDataConnection));
        FastReport.Utils.RegisteredObjects.AddConnection(typeof(JsonDataConnection));
        FastReport.Utils.RegisteredObjects.AddConnection(typeof(ExcelDataConnection));
        FastReport.Utils.RegisteredObjects.AddConnection(typeof(FastReport.ClickHouse.ClickHouseDataConnection));
#endif
    }
  }
}