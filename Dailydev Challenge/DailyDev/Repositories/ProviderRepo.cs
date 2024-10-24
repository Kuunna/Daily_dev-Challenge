﻿using DailyDev.Models;
using System.Data.SqlClient;

namespace DailyDev.Repositories
{
    public class ProviderRepo
    {
        private readonly string _connectionString;

        public ProviderRepo(string connectionString)
        {
            _connectionString = connectionString;
        }
        /*public void Add(Provider provider)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO Provider (Name, Source) VALUES (@Name, @Source)", connection);
                command.Parameters.AddWithValue("@Name", provider.Name);
                command.Parameters.AddWithValue("@Source", provider.Source);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void Update(Provider provider)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE Provider SET Name = @Name, Source = @Source WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", provider.Id);
                command.Parameters.AddWithValue("@Name", provider.Name);
                command.Parameters.AddWithValue("@Source", provider.Source);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        */

        public void Add(Provider provider)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO Provider (Name, Source, Status, ProcessAt) VALUES (@Name, @Source, @Status, @ProcessAt)", connection);
                command.Parameters.AddWithValue("@Name", provider.Name);
                command.Parameters.AddWithValue("@Source", provider.Source);
                command.Parameters.AddWithValue("@Status", provider.Status);
                command.Parameters.AddWithValue("@ProcessAt", provider.ProcessAt);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Provider provider)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE Provider SET Name = @Name, Source = @Source, ProcessId = @ProcessId, Status = @Status, ProcessAt = @ProcessAt WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", provider.Id);
                command.Parameters.AddWithValue("@Name", provider.Name);
                command.Parameters.AddWithValue("@Source", provider.Source);
                command.Parameters.AddWithValue("@ProcessId", provider.ProcessId);
                command.Parameters.AddWithValue("@Status", provider.Status);
                command.Parameters.AddWithValue("@ProcessAt", provider.ProcessAt);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        public IEnumerable<Provider> GetAll()
        {
            var providers = new List<Provider>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Provider", connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        providers.Add(new Provider
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString(),
                            Source = reader["Source"].ToString()
                        });
                    }
                }
            }
            return providers;
        }

        public Provider GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Provider WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Provider
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString(),
                            Source = reader["Source"].ToString()
                        };
                    }
                }
            }
            return null;
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM Provider WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
