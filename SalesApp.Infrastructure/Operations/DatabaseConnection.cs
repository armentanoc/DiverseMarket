
using System.Data.SQLite;

namespace SalesApp.Infrastructure.Operations
{
    internal abstract class DatabaseConnection
    {
        private static string _connectionString = "Data Source=bancotemporario.db;Version=3;";

        protected static SQLiteConnection _connection;

        protected static SQLiteCommand _command;
        public static bool Open()
        {

            if (!File.Exists(@"bancotemporario.db"))
            {
                CreateDB();
                return true;
            }
            else 
            {
                try
                {
                    _connection = new SQLiteConnection(_connectionString);
                    _connection.Open();
                    return true;
                }
                catch (SQLiteException e)
                {
                    Console.WriteLine("An error occured when an attempt to open a connection to this database was made: " + e.Message);
                }
            }
                
            return false;
        }


        private static void CreateDB()
        {
            SQLiteConnection.CreateFile(@"bancotemporario.db");

            _connection = new SQLiteConnection($"{_connectionString}New=True;");
            _connection.Open();

            _command = new SQLiteCommand(_connection);
            _command.CommandText = "CREATE TABLE IF NOT EXISTS `Address` (\r\n    `id` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,\r\n    `street` VARCHAR(45) NOT NULL,\r\n    " +
                "`complement` VARCHAR(45),\r\n    `zipcode` VARCHAR(45) NOT NULL,\r\n    `neighborhood` VARCHAR(45) NOT NULL,\r\n    `city` VARCHAR(45) NOT NULL,\r\n    `state` VARCHAR(45) NOT NULL\r\n);";
            _command.ExecuteNonQuery(); //address table

            _command.CommandText = "CREATE TABLE IF NOT EXISTS `User` (\r\n    `id` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,\r\n    " +
                "`name` VARCHAR(45) NOT NULL,\r\n    `username` VARCHAR(45) NOT NULL UNIQUE,\r\n    `password` VARCHAR(45) NOT NULL,\r\n    `email` VARCHAR(45) NOT NULL,\r\n   " +
                " `telephone` INT,\r\n    `role` TEXT NOT NULL CHECK(role IN ('Seller', 'Client', 'Moderator')),\r\n    `Address_id` INTEGER NOT NULL,\r\n    FOREIGN KEY (`Address_id`) REFERENCES `Address` (`id`)\r\n      " +
                " ON DELETE NO ACTION ON UPDATE NO ACTION\r\n);";
            _command.ExecuteNonQuery();
            InsertInitialData();
        }

        private static void InsertInitialData()
        {
            _command.CommandText = "";
            _command.ExecuteNonQuery();
        }

        protected static bool Close()
        {
            try
            {
                _connection.Close();
                return true;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine("An error occured when an attempt to close a connection to this database was made: " + e.Message);
            }
            return false;
        }
    }
}
