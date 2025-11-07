using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace КП_Кафедра
{
    public static class SqliteDataReaderExtensions
    {
        public static int GetInt32OrDefault(this SqliteDataReader reader, string columnName)
        {
            int ordinal = reader.GetOrdinal(columnName);
            return !reader.IsDBNull(ordinal) ? reader.GetInt32(ordinal) : 0;
        }
    }
}
