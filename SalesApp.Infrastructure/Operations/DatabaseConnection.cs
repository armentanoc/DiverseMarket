
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
            try
            {
                _connection = new SQLiteConnection(_connectionString);

                if (!File.Exists("bancotemporario.db"))
                {
                    CreateDB();
                }
                else
                {
                    _connection.Open();
                    _command = _connection.CreateCommand();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error opening the database: {ex.Message}");
                return false;
            }
        }
        private static void CreateDB()
        {
            try
            {
                SQLiteConnection.CreateFile("bancotemporario.db");
                _connection.Open();

                using (var transaction = _connection.BeginTransaction())
                {
                    _command = _connection.CreateCommand();

                    _command.CommandText = "CREATE TABLE IF NOT EXISTS `Address` (\r\n    `id` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,\r\n    `street` VARCHAR(45) NOT NULL,\r\n    " +
                "`complement` VARCHAR(45),\r\n    `zipcode` VARCHAR(45) NOT NULL,\r\n    `neighborhood` VARCHAR(45) NOT NULL,\r\n    `city` VARCHAR(45) NOT NULL,\r\n    `state` VARCHAR(45) NOT NULL\r\n);";
                    _command.ExecuteNonQuery();
                    Console.WriteLine("Address table created.");

                    _command.CommandText = "CREATE TABLE IF NOT EXISTS `User` (\r\n    `id` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,\r\n    " +
                "`name` VARCHAR(45) NOT NULL,\r\n    `username` VARCHAR(45) NOT NULL UNIQUE,\r\n    `password` VARCHAR(45) NOT NULL,\r\n    `email` VARCHAR(45) NOT NULL,\r\n   " +
                " `telephone` INT,\r\n    `role` TEXT NOT NULL CHECK(role IN ('Seller', 'Client', 'Moderator')),\r\n    `Address_id` INTEGER NOT NULL,\r\n    FOREIGN KEY (`Address_id`) REFERENCES `Address` (`id`)\r\n      " +
                " ON DELETE NO ACTION ON UPDATE NO ACTION\r\n);";
                    _command.ExecuteNonQuery();
                    Console.WriteLine("User table created.");

                    _command.CommandText = ProductDB.CreateTable();
                    _command.ExecuteNonQuery();
                    Console.WriteLine("Product table created.");

                    _command.CommandText = SellingItemDB.CreateTable();
                    _command.ExecuteNonQuery();
                    Console.WriteLine("SellingItem table created.");


                    _command.CommandText = ProductReviewDB.CreateTable();
                    _command.ExecuteNonQuery();
                    Console.WriteLine("ProductReview table created.");

                    // Create other tables as needed

                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating the database: {ex.Message}");
            }
        }

        private static void InsertInitialData()
        {
            _command.CommandText = "";
            _command.ExecuteNonQuery();
        }

        public static bool Close()
        {
            try
            {
                _connection.Close();
                return true;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine($"Error closing the database connection: {e.Message}");
                return false;
            }
        }

        public static void DisplayTableSchema(string tableName)
        {
            try
            {
                if (_command != null)
                {
                    _command.CommandText = $"PRAGMA table_info({tableName})";
                    using (var reader = _command.ExecuteReader())
                    {
                        Console.WriteLine($"\nTable Schema for {tableName}:");
                        Console.WriteLine("Column Name\tType\tNotNull\tPrimaryKey");
                        Console.WriteLine("------------------------------------------");

                        while (reader.Read())
                        {
                            string columnName = reader["name"].ToString();
                            string columnType = reader["type"].ToString();
                            bool notNull = Convert.ToBoolean(reader["notnull"]);
                            bool isPrimaryKey = Convert.ToBoolean(reader["pk"]);

                            Console.WriteLine($"{columnName}\t{columnType}\t{notNull}\t{isPrimaryKey}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("_command is null. Make sure it is properly initialized.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error displaying table schema: {ex.Message}");
            }
        }
    }
}
