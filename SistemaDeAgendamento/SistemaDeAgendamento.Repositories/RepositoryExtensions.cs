using MySql.Data.MySqlClient;
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
    }
}
