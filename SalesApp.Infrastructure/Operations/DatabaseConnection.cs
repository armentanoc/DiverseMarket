using System;
using System.Data.SQLite;
using System.IO;
using SalesApp.Infrastructure.Repositories;

namespace SalesApp.Infrastructure.Operations
{
    internal abstract class DatabaseConnection
    {
        private static string _connectionString = "Data Source=bancotemporario.db;Version=3;";
        protected static SQLiteConnection _connection;
        protected static SQLiteCommand _command;

        internal static bool Open()
        {
            try
            {
                _connection = new SQLiteConnection(_connectionString);

                if (!File.Exists("bancotemporario.db"))
                {
                    Console.WriteLine("Creating a new database file.");
                    CreateDB();
                }
                else
                {
                    _connection.Open();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error opening the database: {ex.Message}");
                Console.ReadKey();
                return false;
            }
        }

        internal static void CreateTables()
        {
            _command = _connection.CreateCommand();
            using (var transaction = _connection.BeginTransaction())
            {
                CreateAndLogTable("Address", AddressDB.InitializeTable);
                CreateAndLogTable("User", UserDB.InitializeTable);
                CreateAndLogTable("Company", CompanyDB.InitializeTable);
                CreateAndLogTable("Product", ProductDB.InitializeTable);
                CreateAndLogTable("ReviewSellingItem", ReviewSellingItemDB.InitializeTable);
                CreateAndLogTable("ProductReview", ProductReviewDB.InitializeTable);

                transaction.Commit();
            }
        }
        internal static void CreateDB()
        {
            try
            {
                SQLiteConnection.CreateFile("bancotemporario.db");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating the database: {ex.Message}");
                Console.ReadKey();
            }
        }

        internal static void CreateAndLogTable(string tableName, Func<string> createTableMethod)
        {
            using (var command = _connection.CreateCommand())
            {
                try
                {
                    if (!TableExists(tableName, command))
                    {
                        string createTableSql = createTableMethod();
                        command.CommandText = createTableSql;
                        command.ExecuteNonQuery();

                        Console.WriteLine($"{tableName} table created.");
                    }
                    else
                    {
                        Console.WriteLine($"{tableName} table already exists.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating {tableName} table: {ex.Message}");
                }
            }
        }

        private static bool TableExists(string tableName, SQLiteCommand command)
        {
            command.CommandText = $"SELECT name FROM sqlite_master WHERE type='table' AND name='{tableName}'";
            object result = command.ExecuteScalar();
            return result != null && result.ToString() == tableName;
        }

        internal static bool Close()
        {
            try
            {
                _connection.Close();
                return true;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine($"Error closing the database connection: {e.Message}");
                Console.ReadKey();
                return false;
            }
        }

        internal static void DisplayTableSchema(string tableName)
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
