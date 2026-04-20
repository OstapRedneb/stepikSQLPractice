using MySql.Data.MySqlClient;
using stepik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stepik.Services
{
    public class CoursesService
    {
        public static List<Course> Get(string fullName)
        {
            using MySqlConnection connection = new MySqlConnection(Constant.ConnectionString);
            connection.Open();

            string sqlQuery =
                "SELECT courses.title, courses.summary, courses.photo " +
                "FROM user_courses " +
                "INNER JOIN users ON users.id = user_courses.user_id " +
                "INNER JOIN courses ON courses.id = user_courses.course_id " +
                "WHERE users.is_active AND users.full_name = @fullName" +
                "ORDER BY courses.last_viewed DESC;";

            using MySqlCommand command = new MySqlCommand(sqlQuery, connection);

            MySqlParameter fullNameParameter = new MySqlParameter("@fullName", MySqlDbType.VarChar);
            fullNameParameter.Value = fullName;

            command.Parameters.Add(fullNameParameter);

            using MySqlDataReader reader = command.ExecuteReader();

            List<Course> courses = new List<Course>();

            while (reader.Read())
            {
                courses.Add(new Course()
                {
                    Title = reader.GetString(0),
                    Summary = reader.IsDBNull(1) ? null : reader.GetString(1),
                    Photo = reader.IsDBNull(2) ? null : reader.GetString(2)
                });
            }
            return courses;
        }
        public static int GetTotalCount()
        {
            using MySqlConnection connection = new MySqlConnection(Constant.ConnectionString);
            connection.Open();

            string sqlQuery = "SELECT COUNT(*) FROM courses;";

            using MySqlCommand command = new MySqlCommand(sqlQuery, connection);

            object totalCountObj = command.ExecuteScalar();

            return (int)(long)totalCountObj;
        }
    }
}
