using DiverseMarket.Backend.Infrastructure.Repositories;
using DiverseMarket.Backend.Model;
using DiverseMarket.Backend.Model.Companies;
using DiverseMarket.Backend.Model.Transactions;
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
                MyLogger.Log.Error($"Erro ao abrir o banco de dados: {ex.Message}\n");
                return false;
            }
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
        MyLogger.Log.Error($"Erro ao fechar a conexão com o banco de dados: {e.Message}");
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
                if (!File.Exists($"{_databaseName}.db"))
                {
                    MyLogger.Log.Error("Criando um novo arquivo de banco.");
                    SQLiteConnection.CreateFile($"{_databaseName}.db");
                    CreateTables();
                }
                else
                {
                    MyLogger.Log.Error("Arquivo de banco de dados já existe.");
                }
            }
            catch (Exception ex)
            {
                MyLogger.Log.Error($"Erro ao criar o banco de dados: {ex.Message}");
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

                        MyLogger.Log.Error($"Tabela {tableName} criada.");
                    }
                    else
                    {
                        MyLogger.Log.Error($"Tabela {tableName} já existe.");
                    }
                }
                catch (Exception ex)
                {
                    MyLogger.Log.Error($"Erro ao criar a tabela {tableName}: {ex.Message}");
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

            UserDB.RegisterCustomer
            (
                "João Silva",
                "joao@silva.com",
                "joao",
                "987654321",
                "12345678901",
                "34567890",
                "Rua XYZ",
                "Cond. Beta, Ap. 202",
                "45",
                "Centro",
                "Rio de Janeiro",
                "Bb67890@" //senha
            );

            UserDB.RegisterCustomer
            (
                "Maria Oliveira",
                "maria@oliveira.com",
                "maria",
                "567890123",
                "98765432109",
                "87654321",
                "Avenida ABC",
                "Cond. Alfa, Ap. 303",
                "78",
                "Barra",
                "Salvador",
                "Cc90123@" //senha
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

        //private static void InsertSampleSellingData()
        //{
        //    Open();

        //    try
        //    {
        //        // Vamos criar uma instância de Product para representar um produto na venda
        //        Product product = new Product("Nome do Produto", "Descrição do Produto", 29.99m);

        //        // Vamos criar uma instância de Customer para representar o cliente que está realizando a compra
        //        Customer customer = new Customer("Nome do Cliente", "cliente@email.com", "username", "123456789", "12345678901", "12345678", "Rua ABC", "Apto 101", "10", "Bairro", "Cidade", "SenhaDoCliente");

        //        // Vamos criar uma instância de Selling para representar a venda
        //        Selling selling = new Selling(DateTime.Now, customer);

        //        // Adicionando itens à venda
        //        selling.AddItem(product, 2); // 2 unidades do produto

        //        // Vamos inserir a venda no banco de dados
        //        InsertSelling(selling);

        //        MyLogger.Log.Error("Dados de venda de exemplo inseridos com sucesso.");
        //    }
        //    catch (Exception ex)
        //    {
        //        MyLogger.Log.Error($"Erro ao inserir dados de venda de exemplo: {ex.Message}");
        //    }
        //    finally
        //    {
        //        Close();
        //    }
        //}

        //private static void InsertSelling(Selling selling)
        //{
        //    // Use a lógica apropriada para inserir os dados da venda no banco de dados
        //    // Por exemplo, você pode usar comandos SQL ou algum método de um repositório

        //    // Exemplo usando um repositório fictício (ajuste conforme necessário)
        //    SellingRepository sellingRepository = new SellingRepository();
        //    sellingRepository.Insert(selling);
        //}


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
                        MyLogger.Log.Error($"\nEsquema da Tabela {tableName}:\n");
                        MyLogger.Log.Error("Nome da Coluna".PadRight(25) + "Tipo".PadRight(15) + "NotNull".PadRight(10) + "PrimaryKey");
                        MyLogger.Log.Error("------------------------------------------------------------");

                        while (reader.Read())
                        {
                            string columnName = reader["name"].ToString();
                            string columnType = reader["type"].ToString();
                            bool notNull = Convert.ToBoolean(reader["notnull"]);
                            bool isPrimaryKey = Convert.ToBoolean(reader["pk"]);

                            MyLogger.Log.Error($"{columnName.PadRight(25)}{columnType.PadRight(15)}{notNull.ToString().PadRight(10)}{isPrimaryKey}");
                        }
                    }
                }
                else
                {
                    MyLogger.Log.Error("_command é nulo. Certifique-se de que está devidamente inicializado.");
                }
            }
            catch (Exception ex)
            {
                MyLogger.Log.Error($"Erro ao exibir o esquema da tabela: {ex.Message}");
            }
        }

        #endregion

    }
}
