using MySql.Data.MySqlClient;
using SistemaDeAgendamento.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeAgendamento.Repositories
{
    public interface IUserRepository
    {
        User? GetByUserName(string username);

        int? Insert(User user);

        int? Update(User user);

        int? Delete(int id);
    }

    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(string connectionString) : base(connectionString)
        {
        }


        public User? GetByUserName(string username)
        {
            User? user = null;

            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = "SELECT user_id, username, password, role_id FROM user WHERE username = @username";

                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("username", username);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new User
                        {
                            Id = reader.GetInt32("user_id"),
                            UserName = reader.GetString("username"),
                            Password = reader.GetString("password"),
                            RoleId = reader.GetInt32("role_id")
                        };
                    }
                }
            }

            return user;
        }

        public int? Insert(User user)
        {
            int? userId = null;

            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = "INSERT INTO user (username, password, role_id) VALUES (@username, @password, @role_id); SELECT LAST_INSERT_ID()";

                var cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("username", user.UserName);
                cmd.Parameters.AddWithValue("password", user.Password);
                cmd.Parameters.AddWithValue("role_id", user.RoleId);

                conn.Open();

                userId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return userId;
        }

        public int? Update(User user)
        {
            int? Affectedrows = null;

            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = "UPDATE user SET username = @username, password = @password WHERE user_id = @user_id";

                var cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("username", user.UserName);
                cmd.Parameters.AddWithValue("password", user.Password);
                cmd.Parameters.AddWithValue("user_id", user.Id);

                conn.Open();

                Affectedrows = cmd.ExecuteNonQuery();
            }

            return Affectedrows;
        }
        public int? Delete(int id)
        {
            int? affectedRows = null;

            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = "DELETE FROM user WHERE user_id = @user_id";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("user_id", id);

                conn.Open();
                affectedRows = cmd.ExecuteNonQuery();
            }

            return affectedRows;
        }
    }
}
