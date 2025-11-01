using MySql.Data.MySqlClient;
using SistemaDeAgendamento.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeAgendamento.Repositories
{
    public interface IAppointmentRepository
    {
        int? Insert(Appointment appointment);

        IList<Appointment> GetAppointmentsByEmployeeAndDate(int employeeId, DateTime date);
    }

    public class AppointmentRepository : BaseRepository, IAppointmentRepository
    {
        public AppointmentRepository(string connectionString) : base(connectionString)
        {

        }

        public int? Insert(Appointment appointment)
        {
            int? appointmentId = null;  

            using (var conn = new MySqlConnection(ConnectionString))
            {
                var query = @"
                    INSERT INTO appointment (start_time, end_time, status, employee_id, service_id, client_id, created_at)
                    VALUES (@startTime, @endTime, @status, @employeeId, @serviceId, @clientId, @createdAt);
                    SELECT LAST_INSERT_ID();";

                var cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@startTime", appointment.StartTime);
                cmd.Parameters.AddWithValue("@endTime", appointment.EndTime);
                cmd.Parameters.AddWithValue("@status", appointment.Status.ToString());
                cmd.Parameters.AddWithValue("@employeeId", appointment.EmployeeId);
                cmd.Parameters.AddWithValue("@serviceId", appointment.ServiceId);
                cmd.Parameters.AddWithValue("@clientId", appointment.ClientId);
                cmd.Parameters.AddWithValue("@createdAt", appointment.CreatedAt);

                conn.Open();
                
                appointmentId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return appointmentId;
        }

        public IList<Appointment> GetAppointmentsByEmployeeAndDate(int employeeId, DateTime date)
        {
            var result = new List<Appointment>();

            using (var conn = new MySqlConnection(ConnectionString))
            {
                var query = @"
                    SELECT appointment_id, start_time, end_time, status, employee_id, service_id, client_id, created_at
                    FROM appointment
                    WHERE employee_id = @employeeId
                      AND DATE(start_time) = @date";

                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("employeeId", employeeId);
                cmd.Parameters.AddWithValue("date", date.Date);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new Appointment
                        {
                            Id = reader.GetInt32("appointment_id"),
                            EmployeeId = reader.GetInt32("employee_id"),
                            ClientId = reader.GetInt32("client_id"),
                            ServiceId = reader.GetInt32("service_id"),
                            Status = (Status)Enum.Parse(typeof(Status), reader.GetString("status"), true),
                            StartTime = reader.GetDateTime("start_time"),
                            EndTime = reader.GetDateTime("end_time"),
                            CreatedAt = reader.GetDateTime("created_at")
                        });
                    }
                }
            }

            return result;
        }
    }
}
