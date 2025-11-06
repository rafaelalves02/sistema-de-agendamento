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
        int? Insert(IList<Availability> availabilities);

        int? Update(IList<Availability> availabilities);

        IList<Availability> GetByEmployeeId(int employeeId);

        int? DeleteByEmployeeId(int employeeId);

        IList<Availability> GetAvailabilitiesByEmployeeAndWeekday(int employeeId, DayOfWeek weekday);

    }
    public class AvailabilityRepository : BaseRepository, IAvailabilityRepository
    {
        public AvailabilityRepository(string connectionString) : base(connectionString)
        {
        }


        public int? Insert(IList<Availability> availabilities)
        {
            int? availabilityId = null;

            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();

                foreach (var availability in availabilities)
                {
                    var query = @"INSERT INTO Availability (employee_id, weekday, start_time, end_time, is_active)
                                  VALUES (@employee_id, @weekday, @start_time, @end_time, @is_active);
                                  SELECT LAST_INSERT_ID();";

                    var cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@employee_id", availability.EmployeeId);
                    cmd.Parameters.AddWithValue("@weekday", availability.WeekDay.ToString());
                    cmd.Parameters.AddWithValue("@start_time", availability.StartTime);
                    cmd.Parameters.AddWithValue("@end_time", availability.EndTime);
                    cmd.Parameters.AddWithValue("@is_active", availability.IsActive);

                    var result = cmd.ExecuteScalar();
                        
                    if (result != null && int.TryParse(result.ToString(), out int id))
                    {
                        availabilityId = id;
                    }
                }
            }
            return availabilityId;
        }

        public int? Update(IList<Availability> availabilities)
        {
            int? AffectedRows = 0;

            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();

                foreach (var availability in availabilities)
                {
                    var query = "UPDATE availability SET start_time = @start_time, end_time = @end_time, is_active = @is_active, weekday = @weekday WHERE availability_id = @availability_id";

                    var cmd = new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@availability_id", availability.Id);
                    cmd.Parameters.AddWithValue("@start_time", availability.StartTime);
                    cmd.Parameters.AddWithValue("@end_time", availability.EndTime);
                    cmd.Parameters.AddWithValue("@is_active", availability.IsActive);
                    cmd.Parameters.AddWithValue("@weekday", availability.WeekDay.ToString());

                    AffectedRows += cmd.ExecuteNonQuery();
                }
            }

            return AffectedRows;

        }

        public IList<Availability> GetByEmployeeId(int employeeId)
        {
            var availabilities = new List<Availability>();

            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();

                var query = @"SELECT availability_id, employee_id, weekday, start_time, end_time, is_active
                              FROM availability
                              WHERE employee_id = @employee_id";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@employee_id", employeeId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var availability = new Availability
                            {
                                Id = reader.GetInt32("availability_id"),
                                EmployeeId = reader.GetInt32("employee_id"),
                                WeekDay = Enum.Parse<WeekDay>(reader.GetString("weekday")),
                                StartTime = reader.GetTimeSpan("start_time"),
                                EndTime = reader.GetTimeSpan("end_time"),
                                IsActive = reader.GetBoolean("is_active")
                            };

                            availabilities.Add(availability);
                        }
                    }
                }
            }

            return availabilities;
        }

        public int? DeleteByEmployeeId(int employeeId)
        {
            int? rowsAffected = null;

            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();

                var query = @"DELETE FROM availability WHERE employee_id = @employee_id";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@employee_id", employeeId);
                    int affected = cmd.ExecuteNonQuery();
                    rowsAffected = affected;
                }
            }

            return rowsAffected;
        }

        public IList<Availability> GetAvailabilitiesByEmployeeAndWeekday(int employeeId, DayOfWeek weekday)
        {
            var result = new List<Availability>();

            using (var conn = new MySqlConnection(ConnectionString))
            {
                var query = @"
                    SELECT availability_id, weekday, start_time, end_time, is_active, employee_id
                    FROM availability
                    WHERE employee_id = @employeeId
                      AND weekday = @weekday
                      AND is_active = 1
                    ORDER BY start_time";

                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("employeeId", employeeId);
                cmd.Parameters.AddWithValue("weekday", weekday.ToString().ToUpper());

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new Availability
                        {
                            Id = reader.GetInt32("availability_id"),
                            EmployeeId = reader.GetInt32("employee_id"),
                            WeekDay = Enum.Parse<SistemaDeAgendamento.Repositories.Entities.WeekDay>(reader.GetString("weekday"), true),
                            StartTime = reader.GetTimeSpan("start_time"),
                            EndTime = reader.GetTimeSpan("end_time"),
                            IsActive = reader.GetBoolean("is_active")
                        });
                    }
                }
            }

            return result;
        }
    }
}
