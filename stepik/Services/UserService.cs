using MySql.Data.MySqlClient;
using stepik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stepik.Services
{
    public class UserService
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
                        command.ExecuteNonQuery();

                        command.Parameters.AddWithValue("@full_name", user.FullName);
                        command.Parameters.AddWithValue("@details", user.Details);
                        command.Parameters.AddWithValue("@join_date", user.JoinDate);
                        command.Parameters.AddWithValue("@avatar", user.Avatar);
                        command.Parameters.AddWithValue("@is_active", user.IsActive);
                    }
                }

                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}
