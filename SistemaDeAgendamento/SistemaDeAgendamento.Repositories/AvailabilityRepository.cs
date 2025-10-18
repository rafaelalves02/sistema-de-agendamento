using EnglishNow.Repositories;
using MySql.Data.MySqlClient;
using SistemaDeAgendamento.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeAgendamento.Repositories
{
    public interface IAvailabilityRepository
    {
        int? Insert(int employeeId, IList<Availability>? availabilities = null);

        //IList<Availability> GetById(int id);
    }
    public class AvailabilityRepository : BaseRepository, IAvailabilityRepository
    {
        public AvailabilityRepository(string connectionString) : base(connectionString)
        {
        }

        public int? Insert(int employeeId, IList<Availability>? availabilities = null)
        {
            int? lastId = null;

            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();

                foreach (WeekDay day in Enum.GetValues(typeof(WeekDay)))
                {
                    var availability = availabilities?.FirstOrDefault(a => a.WeekDay == day);

                    var query = @"INSERT INTO Availability (employee_id, weekday, start_time, end_time, is_active)
                      VALUES (@employee_id, @weekday, @start_time, @end_time, @is_active);
                      SELECT LAST_INSERT_ID();";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@employee_id", employeeId);
                        cmd.Parameters.AddWithValue("@weekday", day.ToString());
                        cmd.Parameters.AddWithValue("@start_time", availability?.StartTime ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@end_time", availability?.EndTime ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@is_active", availability?.IsActive ?? (object)DBNull.Value);

                        var result = cmd.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int id))
                        {
                            lastId = id;
                        }
                    }
                }
            }
            return lastId;
        }


        //public IList<Availability> GetById(int id)
        //{
        //  //  fazer depois do imsert
        //}
    }
}
