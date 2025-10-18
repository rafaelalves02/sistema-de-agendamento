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
    public interface IEmployeeRepository
    {
        int? Insert(Employee employee);

        IList<Employee> Read();

        int? Update(Employee employee);

        int? Delete(int id);

        Employee? GetById(int id);
    }

    public class EmployeeRepository : BaseRepository, IEmployeeRepository
    {
        public EmployeeRepository(string connectionString) : base(connectionString)
        {
        }


        public int? Insert(Employee employee)
        {
            int? employeeId = null;

            using (var conn = new MySqlConnection(ConnectionString))
            {
                var query = "INSERT INTO employee (name, email, phone_number, user_id) VALUES (@name, @email, @phone_number, @user_id); SELECT LAST_INSERT_ID()";

                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("name", employee.Name);
                cmd.Parameters.AddWithValue("email", employee.Email);
                cmd.Parameters.AddWithValue("phone_number", employee.PhoneNumber);
                cmd.Parameters.AddWithValue("user_id", employee.User!.Id);

                conn.Open();

                employeeId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return employeeId;
        }

        public IList<Employee> Read()
        {
            var result = new List<Employee>();

            using (var conn = new MySqlConnection(ConnectionString))
            {
                var query = "SELECT e.employee_id, e.name, e.phone_number, e.email, u.user_id, u.password, u.username FROM employee e INNER JOIN user u WHERE e.user_id = u.user_id ORDER BY e.employee_id";
                    
                var cmd = new MySqlCommand(query, conn);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var employee = new Employee
                        {
                            Id = reader.GetInt32("employee_id"),
                            Name = reader.GetString("name"),
                            PhoneNumber = reader.GetString("phone_number"),
                            Email = reader.GetStringOrNull("email"),
                            User = new User 
                            {
                                Id = reader.GetInt32("user_id"),
                                Password = reader.GetString("password"),
                                UserName = reader.GetString("username")
                            }

                        };

                        result.Add(employee);
                    }
                }
            }

            return result;
        }
        public int? Update(Employee employee)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                var query = "UPDATE employee SET name = @name, phone_number = @phone_number, email = @email WHERE employee_id = @employee_id";

                var cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("employee_id", employee.Id);
                cmd.Parameters.AddWithValue("name", employee.Name);
                cmd.Parameters.AddWithValue("phone_number", employee.PhoneNumber);
                cmd.Parameters.AddWithValue("email", employee.Email);

                conn.Open();

                return cmd.ExecuteNonQuery();
            }
        }
        public int? Delete(int id)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                var query = "DELETE FROM employee WHERE employee_id = @employee_id";

                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("employee_id", id);

                conn.Open();

                return cmd.ExecuteNonQuery();
            }
        }
        public Employee? GetById(int id)
        {
            Employee? employee = null;

            using (var conn = new MySqlConnection(ConnectionString))
            {
                var query = "SELECT e.employee_id, e.name, e.phone_number, e.email, u.user_id, u.username, u.password FROM employee e INNER JOIN user u ON e.user_id = u.user_id WHERE e.employee_id = @id";

                var cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("id", id);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        employee = new Employee
                        {
                            Id = reader.GetInt32("employee_id"),
                            Name = reader.GetString("name"),
                            PhoneNumber = reader.GetString("phone_number"),
                            Email = reader.GetStringOrNull("email"),
                            User = new User
                            {
                                Id = reader.GetInt32("user_id"),
                                UserName = reader.GetString("username"),
                                Password = reader.GetString("password"),
                            }
                        };
                    }
                }
            }
            return employee;
        }

    }
}
