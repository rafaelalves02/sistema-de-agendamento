using MySql.Data.MySqlClient;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeAgendamento.Repositories
{
    public static class RepositoryExtensions
    {
        public static string? GetStringOrNull(this MySqlDataReader reader, string name)
        {
            return reader[name] == DBNull.Value ? null : (string)reader[name];
        }

        public static string? GetStringOrNull(this SqlDataReader reader, string name)
        {
            return reader[name] == DBNull.Value ? null : (string)reader[name];
        }

        // Extensões para SqlDataReader aceitar nome de coluna como string
        public static int GetInt32(this SqlDataReader reader, string name)
        {
            return reader.GetInt32(reader.GetOrdinal(name));
        }

        public static string GetString(this SqlDataReader reader, string name)
        {
            return reader.GetString(reader.GetOrdinal(name));
        }

        public static decimal GetDecimal(this SqlDataReader reader, string name)
        {
            return reader.GetDecimal(reader.GetOrdinal(name));
        }

        public static DateTime GetDateTime(this SqlDataReader reader, string name)
        {
            return reader.GetDateTime(reader.GetOrdinal(name));
        }

        public static bool GetBoolean(this SqlDataReader reader, string name)
        {
            int index = reader.GetOrdinal(name);
            if (reader.IsDBNull(index))
                return false;

            return Convert.ToByte(reader.GetValue(index)) != 0;
        }

        public static TimeSpan GetTimeSpan(this SqlDataReader reader, string name)
        {
            return reader.GetTimeSpan(reader.GetOrdinal(name));
        }
    }
}
