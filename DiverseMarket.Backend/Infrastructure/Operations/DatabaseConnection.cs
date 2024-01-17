using DiverseMarket.Logger;
using DiverseMarket.Backend.Infrastructure.Repositories;
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

        #region Open and close connection
        internal static bool Open()
        {
            try
            {
                _connection = new SQLiteConnection(_connectionString);
                _connection.Open();

                return true;
            }
            catch (Exception ex)
            {
                new LogMessage(ex);
                return false;
            }
        }

        internal static bool Close()
        {
            try
            {
                _command?.Dispose();
                _connection?.Dispose();
                return true;
            }
            catch (SQLiteException ex)
            {
                new LogMessage(ex);
                return false;
            }
        }

        #endregion

        #region Create Methods
        internal static void CreateTables()
        {
            Open();
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
            InsertDefaultCompanyRelatedData();
            Close();
        }

        internal static void CreateDB()
        {
            try
            {
                string databaseFilePath = $"{_databaseName}.db";
                if (!File.Exists(databaseFilePath))
                {
                    new LogMessage("Criando um novo arquivo de banco.");
                    SQLiteConnection.CreateFile(databaseFilePath);
                    CreateTables();
                }
                else
                {
                    new LogMessage("Arquivo de banco de dados já existe.");
                }
            }
            catch (Exception ex)
            {
                new LogMessage(ex);
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

                        new LogMessage($"Tabela {tableName} criada.");
                    }
                    else
                    {
                        new LogMessage($"Tabela {tableName} já existe.");
                    }
                }
                catch (Exception ex)
                {
                    new LogMessage(ex);
                }
            }
        }

        #endregion

        #region Default Data
        private static void InitializeDefaultUsers()
        {
            RegisterDefaultCustomer();
            RegisterDefaultUserCompany();
            RegisterDefaultModerator();
        }

        private static void InsertDefaultCompanyRelatedData()
        {
            ProductCategoryDB.RegisterDefaultProductCategories();
            ProductDB.RegisterDefaultProducts();
            ProductOfferDB.RegisterDefaultProductOffer();

        }

        private static void RegisterDefaultUserCompany()
        {
            Company company = new Company("88222925000128", "CA Tecnologia Ltda.", "TechCA");
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
        #endregion

        #region Helper Methods
        private static bool TableExists(string tableName, SQLiteCommand command)
        {
            command.CommandText = $"SELECT name FROM sqlite_master WHERE type='table' AND name='{tableName}'";
            object result = command.ExecuteScalar();
            return result != null && result.ToString() == tableName;
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
                        new LogMessage($"\nEsquema da Tabela {tableName}:\n" +
                            $"Nome da Coluna".PadRight(25) + "Tipo".PadRight(15) + "NotNull".PadRight(10) + "PrimaryKey" +
                            "------------------------------------------------------------");

                        while (reader.Read())
                        {
                            string columnName = reader["name"].ToString();
                            string columnType = reader["type"].ToString();
                            bool notNull = Convert.ToBoolean(reader["notnull"]);
                            bool isPrimaryKey = Convert.ToBoolean(reader["pk"]);

                            new LogMessage($"{columnName.PadRight(25)}{columnType.PadRight(15)}{notNull.ToString().PadRight(10)}{isPrimaryKey}");
                        }
                    }
                }
                else
                {
                    new LogMessage("_command é nulo. Certifique-se de que está devidamente inicializado.");
                }
            }
            catch (Exception ex)
            {
                new LogMessage(ex);
            }
        }

        #endregion

    }
}
