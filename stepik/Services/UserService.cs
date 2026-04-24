using MySql.Data.MySqlClient;
using stepik.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
            using var connection = new MySqlConnection(Constant.ConnectionString);
            connection.Open();
            var query = @"SELECT * FROM users
                   WHERE full_name = @FullName AND is_active = 1;";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@FullName", fullName);
            using var reader = command.ExecuteReader();
            return reader.Read()
                ? new User
                {
                    FullName = reader.GetString("full_name"),
                    Details = reader.IsDBNull("details") ? null : reader.GetString("details"),
                    JoinDate = reader.GetDateTime("join_date"),
                    Avatar = reader.IsDBNull("avatar") ? null : reader.GetString("avatar"),
                    IsActive = reader.GetBoolean("is_active"),
                    Knowledge = reader.GetInt32("knowledge"),
                    Reputation = reader.GetInt32("reputation"),
                    FollowersCount = reader.GetInt32("followers_count")
                }
                : null;
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
        public static string FormatUserMetrics(int number) 
        {
            using MySqlConnection connection = new MySqlConnection(Constant.ConnectionString);
            connection.Open();

            string functionName = "format_number";

            using MySqlCommand command = new MySqlCommand(functionName, connection);
            command.CommandType = CommandType.StoredProcedure;

            MySqlParameter numberParameter = new MySqlParameter("@number", number) 
            {
                Direction = ParameterDirection.Input
            };
            MySqlParameter resultParameter = new MySqlParameter("@result", MySqlDbType.VarChar)
            {
                Direction = ParameterDirection.ReturnValue
            };

            command.Parameters.Add(numberParameter);
            command.Parameters.Add(resultParameter);

            command.ExecuteNonQuery();

            return resultParameter.Value.ToString() ?? "Ошибка";
        }
        public static DataSet GetUserRating() 
        {
            using MySqlConnection connection = new MySqlConnection(Constant.ConnectionString);
            connection.Open();

            string getSqlQuery = "SELECT full_name, knowledge, reputation " +
                                 "FROM users " +
                                 "WHERE is_active = 1 " +
                                 "ORDER BY knowledge DESC " +
                                 "LIMIT 10;";

            using MySqlDataAdapter adapter = new MySqlDataAdapter(getSqlQuery, connection);
            
            DataSet results = new DataSet();

            adapter.Fill(results);

            return results;
        }
    }
}
