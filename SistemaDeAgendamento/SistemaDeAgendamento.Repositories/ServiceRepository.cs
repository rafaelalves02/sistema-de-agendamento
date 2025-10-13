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
    public interface IServiceRepository
    {
        int? Insert(Service service);

        IList<Service> Read();

        Service? GetById(int id);

        int? Update(Service service);

        int? Delete(int id);
    }

    public class ServiceRepository : BaseRepository, IServiceRepository
    {
        public ServiceRepository(string connectionString) : base(connectionString)
        {
        }

        public int? Insert(Service service)
        {
            int? serviceId = null;

            using (var conn = new MySqlConnection(ConnectionString))
            {
                var query = @"INSERT INTO service (name, price, duration) 
                              VALUES (@name, @price, @duration);
                              SELECT LAST_INSERT_ID();";

                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", service.Name);
                cmd.Parameters.AddWithValue("@price", service.Price);
                cmd.Parameters.AddWithValue("@duration", service.Duration);

                conn.Open();

                serviceId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return serviceId;
        }

        public IList<Service> Read()
        {
            var result = new List<Service>();

            using (var conn = new MySqlConnection(ConnectionString))
            {
                var query = "SELECT service_id, name, price, duration FROM service";

                var cmd = new MySqlCommand(query, conn);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var service = new Service
                        {
                            Id = reader.GetInt32("service_id"),
                            Name = reader.GetString("name"),
                            Price = reader.GetDecimal("price"),
                            Duration = reader.GetInt32("duration")
                        };

                        result.Add(service);
                    }
                }
            }

            return result;
        }

        public Service? GetById(int id)
        {
            Service? service = null;

            using (var conn = new MySqlConnection(ConnectionString))
            {
                var query = "SELECT service_id, name, price, duration FROM service WHERE service_id = @id";

                var cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        service = new Service
                        {
                            Id = reader.GetInt32("service_id"),
                            Name = reader.GetString("name"),
                            Price = reader.GetDecimal("price"),
                            Duration = reader.GetInt32("duration")
                        };
                    }
                }
            }
            return service;
        }

        public int? Update(Service service)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                var query = "UPDATE service SET name = @name, price = @price, duration = @duration WHERE service_id = @service_id";

                var cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("name", service.Name);
                cmd.Parameters.AddWithValue("price", service.Price);
                cmd.Parameters.AddWithValue("duration", service.Duration);
                cmd.Parameters.AddWithValue("service_id", service.Id);

                conn.Open();

                return cmd.ExecuteNonQuery();
            }
        }

        public int? Delete(int id)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                var query = "DELETE FROM service WHERE service_id = @service_id";

                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("service_id", id);

                conn.Open();

                return cmd.ExecuteNonQuery();
            }
        }
    }
}
