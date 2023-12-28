using System;
using System.Data.SQLite;
using System.IO;
using SalesApp.Infrastructure.Repositories;

namespace SalesApp.Infrastructure.Operations
{
    internal sealed class DatabaseConnection
    {
        private static readonly DatabaseConnection instance = new DatabaseConnection();
        private readonly string _databaseName = "bancotemporario";
        private readonly string _connectionString;
        private SQLiteConnection _connection;
        private SQLiteCommand _command;

        public static DatabaseConnection Instance
        {
            get { return instance; }
        }

        private DatabaseConnection()
        {
            _connectionString = $"Data Source={_databaseName}.db;Version=3;";
        }

        public SQLiteConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new SQLiteConnection(_connectionString);
                }
                return _connection;
            }
        }

        internal bool Open()
        {
            try
            {
                if (!File.Exists($"{_databaseName}.db"))
                {
                    Console.WriteLine("Criando um novo arquivo de banco.");
                    CreateDB();
                }
                else
                {
                    Console.WriteLine("Arquivo de banco de dados já existe.");
                }

                Connection.Open();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao abrir o banco de dados: {ex.Message}\n");
                Console.ReadKey();
                return false;
            }
        }

        internal void CreateTables()
        {
            _command = Connection.CreateCommand();
            using (var transaction = Connection.BeginTransaction())
            {
                CreateAndLogTable("Address", AddressDB.InitializeTable);
                CreateAndLogTable("User", UserDB.InitializeTable);
                CreateAndLogTable("Company", CompanyDB.InitializeTable);
                CreateAndLogTable("Customer", CustomerDB.InitializeTable);
                CreateAndLogTable("ProductCategory", ProductCategoryDB.InitializeTable);
                CreateAndLogTable("Product", ProductDB.InitializeTable);
                CreateAndLogTable("ProductOffer", ProductOfferDB.InitializeTable);
                CreateAndLogTable("ProductReview", ProductReviewDB.InitializeTable);
                CreateAndLogTable("ReviewCompany", ReviewCompanyDB.InitializeTable);
                CreateAndLogTable("ReviewSellingItem", ReviewSellingItemDB.InitializeTable);
                CreateAndLogTable("Selling", SellingDB.InitializeTable);
                CreateAndLogTable("WalletTransactions", WalletTransactionsDB.InitializeTable);

                transaction.Commit();
            }
        }

        internal void CreateDB()
        {
            try
            {
                SQLiteConnection.CreateFile($"{_databaseName}.db");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar o banco de dados: {ex.Message}");
                Console.ReadKey();
            }
        }

        internal void CreateAndLogTable(string tableName, Func<string> createTableMethod)
        {
            using (var command = Connection.CreateCommand())
            {
                try
                {
                    if (!TableExists(tableName, command))
                    {
                        string createTableSql = createTableMethod();
                        command.CommandText = createTableSql;
                        command.ExecuteNonQuery();

                        Console.WriteLine($"Tabela {tableName} criada.");
                    }
                    //else
                    //{
                    //    Console.WriteLine($"Tabela {tableName} já existe.");
                    //}
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao criar a tabela {tableName}: {ex.Message}");
                }
            }
        }

        private bool TableExists(string tableName, SQLiteCommand command)
        {
            command.CommandText = $"SELECT name FROM sqlite_master WHERE type='table' AND name='{tableName}'";
            object result = command.ExecuteScalar();
            return result != null && result.ToString() == tableName;
        }

        internal bool Close()
        {
            try
            {
                Connection.Close();
                return true;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine($"Erro ao fechar a conexão com o banco de dados: {e.Message}");
                Console.ReadKey();
                return false;
            }
        }

        internal void DisplayTableSchema(string tableName)
        {
            try
            {
                if (_command != null)
                {
                    _command.CommandText = $"PRAGMA table_info({tableName})";
                    using (var reader = _command.ExecuteReader())
                    {
                        Console.WriteLine($"\nEsquema da Tabela {tableName}:\n");
                        Console.WriteLine("Nome da Coluna".PadRight(25) + "Tipo".PadRight(15) + "NotNull".PadRight(10) + "PrimaryKey");
                        Console.WriteLine("------------------------------------------------------------");

                        while (reader.Read())
                        {
                            string columnName = reader["name"].ToString();
                            string columnType = reader["type"].ToString();
                            bool notNull = Convert.ToBoolean(reader["notnull"]);
                            bool isPrimaryKey = Convert.ToBoolean(reader["pk"]);

                            Console.WriteLine($"{columnName.PadRight(25)}{columnType.PadRight(15)}{notNull.ToString().PadRight(10)}{isPrimaryKey}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("_command é nulo. Certifique-se de que está devidamente inicializado.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao exibir o esquema da tabela: {ex.Message}");
            }
        }
    }
}
