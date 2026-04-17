using MySql.Data.MySqlClient;
using stepik.Models;
using stepik.Services;
using System.Reflection.Metadata;
using Constant = stepik.Constant;

public class Program
{
    public static void Main()
    {
        while (true) 
        {
            Console.WriteLine(@"
************************************************
* Добро пожаловать на онлайн платформу Stepik! *
************************************************

Выберите действие (введите число и нажмите Enter):

1. Зарегистрироваться
2. Закрыть приложение

************************************************
");
            string input = Console.ReadLine();

            if (!(input == "1" || input == "2"))
            {
                Console.WriteLine("Неверный выбор. Попробуйте снова.");
                Console.Clear();
                continue;
            }

            if (input == "1")
            {
                Console.Clear();
                RegisterUser();
            }
            if (input == "2") 
            {
                Console.WriteLine("До свидания!");
                break;
            }
        }
    }

    public static void CreateTable() 
    {
        using (MySqlConnection connection = new MySqlConnection(Constant.ConnectionString))
        {
            connection.Open();

            string sqlQuery = "CREATE TABLE IF NOT EXISTS users(" +
                "id         INT PRIMARY KEY AUTO_INCREMENT," +
                "full_name  VARCHAR(100) NOT NULL," +
                "details    TEXT NULL," +
                "join_date  TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP()," +
                "avatar     TEXT NULL," +
                "is_active  BOOLEAN NOT NULL" +
                ");";

            using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
            {
                int execute = command.ExecuteNonQuery();
                Console.WriteLine($"Выполнилось создание таблицы. Добавено {execute} строк");
            }
        }
    }
    public static void RegisterUser() 
    {
        Console.WriteLine("Введите имя и фамилию через пробел и нажмите Enter:");
        string input = Console.ReadLine() ?? "";
        User user = new User(){ FullName = input};

        if (UsersService.Add(user))
            Console.WriteLine($"Пользователь '{user.FullName}' успешно добавлен.\n");
        else
            Console.WriteLine("Произошла ошибка, произведен выход на главную страницу\n");
    }
}