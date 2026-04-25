using MySql.Data.MySqlClient;
using System.Data;
using System.Reflection.Metadata;
using System.Security.Cryptography;

public class Program
{
    public static void Main()
    {
        var menu = new MainMenu();
        menu.Display();
        menu.HandleUserChoice();
    }
}