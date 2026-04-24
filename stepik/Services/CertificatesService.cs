using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace stepik.Services
{
    public class CertificatesService
    {
        public static DataSet Get(string fullName) 
        {
            using MySqlConnection connection = new MySqlConnection(Constant.ConnectionString);
            connection.Open();

            string sqlQuery = "SELECT courses.title, certificates.issue_date, certificates.grade " +
                              "FROM certificates " +
                              "INNER JOIN users ON users.id = certificates.user_id " +
                              "WHERE users.full_name = @name " +
                              "ORDER BY certificates.issue_date DESC;";

            using MySqlCommand command = new MySqlCommand(sqlQuery, connection);

            MySqlParameter nameParameter = new MySqlParameter("@name", MySqlDbType.VarChar) {Value = fullName };
            command.Parameters.Add(nameParameter);

            using MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataSet dataSet = new DataSet("certificates");

            adapter.Fill(dataSet);

            return dataSet;
        }
    }
}
