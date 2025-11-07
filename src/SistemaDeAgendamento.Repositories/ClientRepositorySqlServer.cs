using Microsoft.Data.SqlClient;
using SistemaDeAgendamento.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeAgendamento.Repositories
{
    public class ClientRepositorySqlServer : BaseRepository, IClientRepository
    {
        public ClientRepositorySqlServer(string connectionString) : base(connectionString)
        {

        }

        public Client? GetByPhoneNumber(string phoneNumber)
        {
            Client? client = null;

            using (var conn = new SqlConnection(ConnectionString))
            {
                var query = "SELECT client_id, name, phone_number FROM client WHERE phone_number = @phone_number";

                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@phone_number", phoneNumber);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        client = new Client
                        {
                            Id = reader.GetInt32("client_id"),
                            Name = reader.GetString("name"),
                            PhoneNumber = reader.GetString("phone_number"),
                        };
                    }
                }
            }
            return client;
        }

        public int? Insert(Client client)
        {
            int? clientId = null;

            using (var conn = new SqlConnection(ConnectionString))
            {
                var query = @"INSERT INTO client (name, phone_number) 
                              OUTPUT INSERTED.client_id
                              VALUES (@name, @phone_number);";

                var cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@name", client.Name);
                cmd.Parameters.AddWithValue("@phone_number", client.PhoneNumber);

                conn.Open();

                clientId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return clientId;
        }

        public Client? GetById(int id)
        {
            Client? client = null;

            using (var conn = new SqlConnection(ConnectionString))
            {
                var query = "SELECT client_id, name, phone_number FROM client WHERE client_id = @client_id";

                var cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@client_id", id);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        client = new Client
                        {
                            Id = reader.GetInt32("client_id"),
                            Name = reader.GetString("name"),
                            PhoneNumber = reader.GetString("phone_number"),
                        };
                    }
                }
            }

            return client;
        }
    }
}

