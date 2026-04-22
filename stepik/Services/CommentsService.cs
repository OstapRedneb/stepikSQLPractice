using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using stepik.Models;

namespace stepik.Services
{
    public class CommentsService
    {
        public static List<Comment> Get(int courseId) 
        {
            List<Comment> comments = new List<Comment>();

            using MySqlConnection connection = new MySqlConnection(Constant.ConnectionString);
            connection.Open();

            MySqlTransaction transaction = connection.BeginTransaction();

            try 
            {
                string sqlQuery = "SELECT comments.id, comments.text, comments.time " +
                                  "FROM comments " +
                                  "INNER JOIN steps ON steps.id = comments.step_id " +
                                  "INNER JOIN lessons ON lessons.id = steps.lesson_id " +
                                  "INNER JOIN unit_lessons ON unit_lessons.lesson_id = lessons_id " +
                                  "INNER JOIN units ON units.id = unit_lessons.unit_id " +
                                  "INNER JOIN courses ON courses.id = units.course_id " +
                                  "WHERE course_id = @course_id;";

                using MySqlCommand command = new MySqlCommand(sqlQuery, connection, transaction);

                MySqlParameter courseIdParameter = new MySqlParameter("@course_id", MySqlDbType.Int32) {Value = courseId};

                command.Parameters.Add(courseIdParameter);

                using MySqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    comments.Add(new Comment() 
                        {
                            Id = reader.GetInt32(0),
                            Text = reader.GetString(1),
                            Time = reader.GetDateTime(2)
                        });
                }

                transaction.Commit();
            }
            catch(Exception ex) 
            {
                transaction.Rollback();
            }

            return comments;
        }
    }
}
