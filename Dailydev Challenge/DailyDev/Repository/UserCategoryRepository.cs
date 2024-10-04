﻿using DailyDev.Models;
using System.Data.SqlClient;

namespace DailyDev.Repository
{
    public class UserCategoryRepository
    {
        private readonly string _connectionString;

        public UserCategoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(UserCategory userCategory)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO UserCategory (UserId, CategoryId) VALUES (@UserId, @CategoryId)", connection);
                command.Parameters.AddWithValue("@UserId", userCategory.UserId);
                command.Parameters.AddWithValue("@CategoryId", userCategory.CategoryId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<UserCategory> GetAll()
        {
            var userCategories = new List<UserCategory>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM UserCategory", connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        userCategories.Add(new UserCategory
                        {
                            Id = (int)reader["Id"],
                            UserId = (int)reader["UserId"],
                            CategoryId = (int)reader["CategoryId"]
                        });
                    }
                }
            }
            return userCategories;
        }

        public UserCategory GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM UserCategory WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new UserCategory
                        {
                            Id = (int)reader["Id"],
                            UserId = (int)reader["UserId"],
                            CategoryId = (int)reader["CategoryId"]
                        };
                    }
                }
            }
            return null;
        }

        public void Update(UserCategory userCategory)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE UserCategory SET UserId = @UserId, CategoryId = @CategoryId WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", userCategory.Id);
                command.Parameters.AddWithValue("@UserId", userCategory.UserId);
                command.Parameters.AddWithValue("@CategoryId", userCategory.CategoryId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM UserCategory WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }


}