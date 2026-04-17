using MySql.Data.MySqlClient;
using System.Reflection.Metadata;
using Constant = stepik.Constant;

public class Program
{
    public static void Main()
    {
        using (MySqlConnection connection = new MySqlConnection(Constant.ConnectionString))
        {
            connection.Open();

            string sqlQuery = "CREATE TABLE IF NOT EXISTS users(" +
                "id         INT PRIMARY KEY AUTO_INCREMENT," +
                "full_name  VARCHAR(100) NOT NULL," +
                "details    TEXT NULL," +
                "join_date  TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP()," +
                "avatar     TEXT NOT NULL," +
                "is_active  BOOLEAN NOT NULL" +
                ");";

            using(MySqlCommand command = new MySqlCommand(sqlQuery, connection)) 
            {
                int execute = command.ExecuteNonQuery();
                Console.WriteLine($"Выполнилось создание таблицы. Добавено {execute} строк");
            }
        }
    }
}