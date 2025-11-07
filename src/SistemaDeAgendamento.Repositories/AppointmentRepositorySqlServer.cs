using Microsoft.Data.SqlClient;
using SistemaDeAgendamento.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeAgendamento.Repositories
{
    public class AppointmentRepositorySqlServer : BaseRepository, IAppointmentRepository
    {
        public AppointmentRepositorySqlServer(string connectionString) : base(connectionString)
        {

        }

        public int? Insert(Appointment appointment)
        {
            int? appointmentId = null;  

            using (var conn = new SqlConnection(ConnectionString))
            {
                var query = @"
                    INSERT INTO appointment (start_time, end_time, status, employee_id, service_id, client_id, created_at)
                    OUTPUT INSERTED.appointment_id
                    VALUES (@startTime, @endTime, @status, @employeeId, @serviceId, @clientId, @createdAt);";

                var cmd = new SqlCommand(query, conn);

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

        public IList<Appointment> Read()
        {
            var result = new List<Appointment>();

            using (var conn = new SqlConnection(ConnectionString))
            {
                var query = @"
                    SELECT 
                        a.appointment_id,
                        a.client_id,
                        a.service_id,
                        a.employee_id,
                        a.status,
                        a.start_time,
                        a.end_time,
                        a.created_at,
                        c.name AS client_name,
                        c.phone_number AS client_phone,
                        s.name AS service_name,
                        s.price AS service_price,
                        s.duration AS service_duration,
                        e.name AS employee_name,
                        e.phone_number AS employee_phone
                    FROM appointment a
                    JOIN client c ON a.client_id = c.client_id
                    JOIN service s ON a.service_id = s.service_id
                    JOIN employee e ON a.employee_id = e.employee_id
                    ORDER BY a.start_time DESC";

                var cmd = new SqlCommand(query, conn);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new Appointment
                        {
                            Id = reader.GetInt32("appointment_id"),
                            ClientId = reader.GetInt32("client_id"),
                            ServiceId = reader.GetInt32("service_id"),
                            EmployeeId = reader.GetInt32("employee_id"),
                            Status = (Status)Enum.Parse(typeof(Status), reader.GetString("status"), true),
                            StartTime = reader.GetDateTime("start_time"),
                            EndTime = reader.GetDateTime("end_time"),
                            CreatedAt = reader.GetDateTime("created_at"),
                            Client = new Client
                            {
                                Id = reader.GetInt32("client_id"),
                                Name = reader.GetString("client_name"),
                                PhoneNumber = reader.GetString("client_phone")
                            },
                            Service = new Service
                            {
                                Id = reader.GetInt32("service_id"),
                                Name = reader.GetString("service_name"),
                                Price = reader.GetDecimal("service_price"),
                                Duration = reader.GetInt32("service_duration")
                            },
                            Employee = new Employee
                            {
                                Id = reader.GetInt32("employee_id"),
                                Name = reader.GetString("employee_name"),
                                PhoneNumber = reader.GetString("employee_phone")
                            }
                        });
                    }
                }
            }

            return result;
        }

        public IList<Appointment> GetByEmployeeId(int employeeId)
        {
            var result = new List<Appointment>();

            using (var conn = new SqlConnection(ConnectionString))
            {
                var query = @"
                    SELECT 
                        a.appointment_id,
                        a.client_id,
                        a.service_id,
                        a.employee_id,
                        a.status,
                        a.start_time,
                        a.end_time,
                        a.created_at,
                        c.name AS client_name,
                        c.phone_number AS client_phone,
                        s.name AS service_name,
                        s.price AS service_price,
                        s.duration AS service_duration,
                        e.name AS employee_name,
                        e.phone_number AS employee_phone
                    FROM appointment a
                    INNER JOIN client c ON a.client_id = c.client_id
                    INNER JOIN service s ON a.service_id = s.service_id
                    INNER JOIN employee e ON a.employee_id = e.employee_id
                    WHERE a.employee_id = @employeeId
                    ORDER BY a.start_time DESC";

                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@employeeId", employeeId);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new Appointment
                        {
                            Id = reader.GetInt32("appointment_id"),
                            ClientId = reader.GetInt32("client_id"),
                            ServiceId = reader.GetInt32("service_id"),
                            EmployeeId = reader.GetInt32("employee_id"),
                            Status = (Status)Enum.Parse(typeof(Status), reader.GetString("status"), true),
                            StartTime = reader.GetDateTime("start_time"),
                            EndTime = reader.GetDateTime("end_time"),
                            CreatedAt = reader.GetDateTime("created_at"),
                            Client = new Client
                            {
                                Id = reader.GetInt32("client_id"),
                                Name = reader.GetString("client_name"),
                                PhoneNumber = reader.GetString("client_phone")
                            },
                            Service = new Service
                            {
                                Id = reader.GetInt32("service_id"),
                                Name = reader.GetString("service_name"),
                                Price = reader.GetDecimal("service_price"),
                                Duration = reader.GetInt32("service_duration")
                            },
                            Employee = new Employee
                            {
                                Id = reader.GetInt32("employee_id"),
                                Name = reader.GetString("employee_name"),
                                PhoneNumber = reader.GetString("employee_phone")
                            }
                        });
                    }
                }
            }

            return result;
        }

        public IList<Appointment> GetByClientPhoneNumber(string phoneNumber)
        {
            var result = new List<Appointment>();

            using (var conn = new SqlConnection(ConnectionString))
            {
                var query = @"
                    SELECT 
                        a.appointment_id,
                        a.client_id,
                        a.service_id,
                        a.employee_id,
                        a.status,
                        a.start_time,
                        a.end_time,
                        a.created_at,
                        c.name AS client_name,
                        c.phone_number AS client_phone,
                        s.name AS service_name,
                        s.price AS service_price,
                        s.duration AS service_duration,
                        e.name AS employee_name,
                        e.phone_number AS employee_phone
                    FROM appointment a
                    INNER JOIN client c ON a.client_id = c.client_id
                    INNER JOIN service s ON a.service_id = s.service_id
                    INNER JOIN employee e ON a.employee_id = e.employee_id
                    WHERE c.phone_number = @phoneNumber
                    ORDER BY a.start_time DESC";

                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@phoneNumber", phoneNumber);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new Appointment
                        {
                            Id = reader.GetInt32("appointment_id"),
                            ClientId = reader.GetInt32("client_id"),
                            ServiceId = reader.GetInt32("service_id"),
                            EmployeeId = reader.GetInt32("employee_id"),
                            Status = (Status)Enum.Parse(typeof(Status), reader.GetString("status"), true),
                            StartTime = reader.GetDateTime("start_time"),
                            EndTime = reader.GetDateTime("end_time"),
                            CreatedAt = reader.GetDateTime("created_at"),
                            Client = new Client
                            {
                                Id = reader.GetInt32("client_id"),
                                Name = reader.GetString("client_name"),
                                PhoneNumber = reader.GetString("client_phone")
                            },
                            Service = new Service
                            {
                                Id = reader.GetInt32("service_id"),
                                Name = reader.GetString("service_name"),
                                Price = reader.GetDecimal("service_price"),
                                Duration = reader.GetInt32("service_duration")
                            },
                            Employee = new Employee
                            {
                                Id = reader.GetInt32("employee_id"),
                                Name = reader.GetString("employee_name"),
                                PhoneNumber = reader.GetString("employee_phone")
                            }
                        });
                    }
                }
            }

            return result;
        }

        public IList<Appointment> GetAppointmentsByEmployeeAndDate(int employeeId, DateTime date)
        {
            var result = new List<Appointment>();

            using (var conn = new SqlConnection(ConnectionString))
            {
                var query = @"
                    SELECT appointment_id, start_time, end_time, status, employee_id, service_id, client_id, created_at
                    FROM appointment
                    WHERE employee_id = @employeeId
                      AND CAST(start_time AS DATE) = CAST(@date AS DATE)";

                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@employeeId", employeeId);
                cmd.Parameters.AddWithValue("@date", date.Date);

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

