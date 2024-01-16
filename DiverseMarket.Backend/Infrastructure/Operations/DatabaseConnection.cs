﻿using DiverseMarket.Backend.Infrastructure.Repositories;
using DiverseMarket.Backend.Model;
using DiverseMarket.Backend.Model.Companies;
using System.Data.SQLite;

namespace DiverseMarket.Backend.Infrastructure.Operations
{
    public abstract class DatabaseConnection
    {
        private static string _databaseName = "bancotemporario";
        private static string _connectionString = $"Data Source={_databaseName}.db;Version=3;";
        protected static SQLiteConnection _connection;
        protected static SQLiteCommand _command;

        internal static bool Open()
        {
            try
            {
                _connection = new SQLiteConnection(_connectionString);

                if (!File.Exists($"{_databaseName}.db"))
                {
                    Console.WriteLine("Criando um novo arquivo de banco.\n");
                    CreateDB();
                }
                else
                {
                    Console.WriteLine("Arquivo de banco de dados já existe.\n");
                }

                _connection.Open();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao abrir o banco de dados: {ex.Message}\n");
                Console.ReadKey();
                return false;
            }
        }

        internal static void CreateTables()
        {
            _connection.Open();
            _command = _connection.CreateCommand();
            using (var transaction = _connection.BeginTransaction())
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

            InitializeDefaultUsers();
            _connection.Close();
        }

        private static void InitializeDefaultUsers()
        {
            RegisterDefaultCustomer();
            RegisterDefaultUserCompany();
            RegisterDefaultModerator();
        }

        private static void RegisterDefaultUserCompany()
        {
            Company company = new Company(1, "88222925000128", "CA Tecnologia Ltda.", "TechCA");
            UserDB.RegisterCompany(
            company,
            new Address("40280000", "Avenida Antonio Carlos Magalhães", "1234", "Brotas", "Salvador", "Cond. Ômega"),
            "tech@ca.com",
            "7133581234",
            "carolina",
            "Aa12345@" //senha
            );
        }

        private static void RegisterDefaultCustomer()
        {
            UserDB.RegisterCustomer
            (
            "Vitória Lira",
            "vitoria@vitoria.com",
            "vitoria",
            "123456789",
            "73883712060",
            "24330350",
            "Estrada E",
            "Cond. Gama, Ap. 101",
            "13",
            "Várzea das Moças",
            "Niterói",
            "Aa12345@" //senha
            );
        }
            private static void RegisterDefaultModerator()
            {
                UserDB.RegisterModerator
                (
                "Paula Andrezza",
                "paula@gmail.com",
                "paula",
                "123456789",
                "89403309008",
                "53620819",
                "Rua Senhor do Bonfim",
                "Cond. Delta, Ap. 202",
                "12",
                "Santa Rita",
                "Igarassu",
                "Aa12345@" //senha
                );
            }

        internal static void CreateDB()
        {
            try
            {
                SQLiteConnection.CreateFile($"{_databaseName}.db");
                CreateTables();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar o banco de dados: {ex.Message}");
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

                        Console.WriteLine($"Tabela {tableName} criada.");
                    }
                    else
                    {
                        Console.WriteLine($"Tabela {tableName} já existe.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao criar a tabela {tableName}: {ex.Message}");
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
                Console.WriteLine($"Erro ao fechar a conexão com o banco de dados: {e.Message}");
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
