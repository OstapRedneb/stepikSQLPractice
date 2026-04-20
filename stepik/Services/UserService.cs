using MySql.Data.MySqlClient;
using stepik.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace stepik.Services
{
    public class UsersService
    {
        public static bool Add(User user) 
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Constant.ConnectionString))
                {
                    connection.Open();

                    string sqlQuery = @"INSERT INTO users(full_name, details, join_date, avatar, is_active) " +
                        "VALUES (@full_name, @details, @join_date, @avatar, @is_active);";

                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@full_name", user.FullName);
                        command.Parameters.AddWithValue("@details", user.Details);
                        command.Parameters.AddWithValue("@join_date", user.JoinDate);
                        command.Parameters.AddWithValue("@avatar", user.Avatar);
                        command.Parameters.AddWithValue("@is_active", user.IsActive);

                        command.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch 
            {
                return false;
            }
        }
        public static User? Get(string fullName) 
        {
            using MySqlConnection connection = new MySqlConnection(Constant.ConnectionString);
            connection.Open();

            string sqlQuery = $"SELECT * FROM users WHERE full_name = @fullName AND is_active = 1;";

            using MySqlCommand command = new MySqlCommand(sqlQuery, connection);

            command.Parameters.AddWithValue("@fullName", fullName);

            using MySqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new User
                {
                    FullName = reader.IsDBNull(1) ? null : reader.GetString(1),
                    Details = reader.IsDBNull(2) ? null : reader.GetString(2),
                    JoinDate = reader.GetDateTime(3),
                    Avatar = reader.IsDBNull(4) ? null : reader.GetString(4),
                    IsActive = reader.GetBoolean(5)
                };
            }
            return null;
        }
        public static int GetTotalCount() 
        {
            using MySqlConnection connection = new MySqlConnection(Constant.ConnectionString);
            connection.Open();

            string sqlQuery = "SELECT COUNT(*) FROM users;";

            using MySqlCommand command = new MySqlCommand(sqlQuery, connection);

            object countObj = command.ExecuteScalar();

            return (int)(long)countObj;
        }
    }
}
