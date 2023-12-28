
using System.Data.SQLite;

namespace SalesApp.Infrastructure.Operations
{
    public abstract class DatabaseConnection
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

            // Company table
            _command.CommandText = @"CREATE TABLE IF NOT EXISTS Company (
                            id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                            CNPJ VARCHAR(14) NOT NULL,
                            corporate_name VARCHAR(45) NOT NULL,
                            trade_name VARCHAR(45) NOT NULL,
                            legal_entity_type_code INTEGER,
                            User_id INTEGER NOT NULL,
                            UNIQUE(CNPJ),
                            FOREIGN KEY (User_id) REFERENCES User (id)
                                ON DELETE NO ACTION
                                ON UPDATE NO ACTION
                        );";
            _command.ExecuteNonQuery();

            // WalletTransactions table
            _command.CommandText = @"CREATE TABLE IF NOT EXISTS WalletTransactions (
                            id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                            amount DECIMAL(10,2) NOT NULL,
                            date_transaction DATETIME NOT NULL,
                            type TEXT NOT NULL CHECK(type IN ('Debit', 'Credit', 'Withdrawal'))
                        );";
            _command.ExecuteNonQuery();

            // Client table
            _command.CommandText = @"CREATE TABLE IF NOT EXISTS Client (
                            id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                            User_id INTEGER NOT NULL,
                            cpf VARCHAR(45) NOT NULL,
                            WalletTransactions_id INTEGER NOT NULL,
                            FOREIGN KEY (User_id) REFERENCES User (id)
                                ON DELETE NO ACTION
                                ON UPDATE NO ACTION,
                            FOREIGN KEY (WalletTransactions_id) REFERENCES WalletTransactions (id)
                                ON DELETE NO ACTION
                                ON UPDATE NO ACTION
                        );";
            _command.ExecuteNonQuery();

            // Product table
            _command.CommandText = @"CREATE TABLE IF NOT EXISTS Product (
                            id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                            name VARCHAR(45) NOT NULL,
                            description VARCHAR(45)
                        );";
            _command.ExecuteNonQuery();

            // Selling table
            _command.CommandText = @"CREATE TABLE IF NOT EXISTS Selling (
                            id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                            date_sale DATE NOT NULL,
                            amount DECIMAL(10,2),
                            Client_id INTEGER NOT NULL,
                            date_EndSale DATETIME,
                            FOREIGN KEY (Client_id) REFERENCES Client (id)
                                ON DELETE NO ACTION
                                ON UPDATE NO ACTION
                        );";
            _command.ExecuteNonQuery();

            // SellingItem table
            _command.CommandText = @"CREATE TABLE IF NOT EXISTS SellingItem (
                            Selling_id INTEGER NOT NULL,
                            Product_id INTEGER NOT NULL,
                            quantity INTEGER NOT NULL,
                            price DECIMAL(10,2) NOT NULL,
                            Company_id INTEGER NOT NULL,
                            PRIMARY KEY (Selling_id, Product_id),
                            FOREIGN KEY (Selling_id) REFERENCES Selling (id)
                                ON DELETE NO ACTION
                                ON UPDATE NO ACTION,
                            FOREIGN KEY (Product_id) REFERENCES Product (id)
                                ON DELETE NO ACTION
                                ON UPDATE NO ACTION,
                            FOREIGN KEY (Company_id) REFERENCES Company (id)
                                ON DELETE NO ACTION
                                ON UPDATE NO ACTION
                        );";
            _command.ExecuteNonQuery();

            // ProductOffer table
            _command.CommandText = @"CREATE TABLE IF NOT EXISTS ProductOffer (
                            Company_id INTEGER NOT NULL,
                            Product_id INTEGER NOT NULL,
                            price DECIMAL(10,2) NOT NULL,
                            quantity INTEGER NOT NULL,
                            PRIMARY KEY (Company_id, Product_id),
                            FOREIGN KEY (Company_id) REFERENCES Company (id)
                                ON DELETE NO ACTION
                                ON UPDATE NO ACTION,
                            FOREIGN KEY (Product_id) REFERENCES Product (id)
                                ON DELETE NO ACTION
                                ON UPDATE NO ACTION
                        );";
            _command.ExecuteNonQuery();

            // ReviewSellingItem table
            _command.CommandText = @"CREATE TABLE IF NOT EXISTS ReviewSellingItem (
                            id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                            note INTEGER NOT NULL,
                            comment VARCHAR(45) NOT NULL,
                            SellingItem_Selling_id INTEGER NOT NULL,
                            SellingItem_Product_id INTEGER NOT NULL,
                            FOREIGN KEY (SellingItem_Selling_id, SellingItem_Product_id) REFERENCES SellingItem (Selling_id, Product_id)
                                ON DELETE NO ACTION
                                ON UPDATE NO ACTION
                        );";
            _command.ExecuteNonQuery();

            // ReviewCompany table
            _command.CommandText = @"CREATE TABLE IF NOT EXISTS ReviewCompany (
                            id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                            Client_id INTEGER NOT NULL,
                            Company_id INTEGER NOT NULL,
                            note INTEGER NOT NULL,
                            comment VARCHAR(45) NOT NULL,
                            FOREIGN KEY (Client_id) REFERENCES Client (id)
                                ON DELETE NO ACTION
                                ON UPDATE NO ACTION,
                            FOREIGN KEY (Company_id) REFERENCES Company (id)
                                ON DELETE NO ACTION
                                ON UPDATE NO ACTION
                        );";
            _command.ExecuteNonQuery();

            // Refund table
            _command.CommandText = @"CREATE TABLE IF NOT EXISTS Refund (
                            id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                            date_opening DATETIME NOT NULL,
                            date_closure DATETIME NOT NULL,
                            status TEXT NOT NULL CHECK(status IN ('Open', 'AnalysisCompany', 'AnalysisModerator', 'Closed')),
                            isAccepted TINYINT,
                            SellingItem_Selling_id INTEGER NOT NULL,
                            SellingItem_Product_id INTEGER NOT NULL,
                            FOREIGN KEY (SellingItem_Selling_id, SellingItem_Product_id) REFERENCES SellingItem (Selling_id, Product_id)
                                ON DELETE NO ACTION
                                ON UPDATE NO ACTION
                        );";
            _command.ExecuteNonQuery();
            InsertInitialData();
        }

        private static void InsertInitialData()
        {
            // Insert data into the Address table
            _command.CommandText = @"INSERT INTO Address (street, complement, zipcode, neighborhood, city, state) 
                             VALUES ('123 Main St', 'Apt 4', '12345-678', 'Downtown', 'Cityville', 'CA')";
            _command.ExecuteNonQuery();

            // Insert data into the User table
            _command.CommandText = @"INSERT INTO User (name, username, password, email, telephone, role, Address_id) 
                             VALUES ('John Doe', 'john_doe', 'password123', 'john@example.com', 1234567890, 'Client', 1)";
            _command.ExecuteNonQuery();

            // Insert data into the Company table
            _command.CommandText = @"INSERT INTO Company (CNPJ, corporate_name, trade_name, legal_entity_type_code, User_id) 
                             VALUES ('12345678901234', 'ABC Corp', 'ABC', 1234, 1)";
            _command.ExecuteNonQuery();

            // Insert data into the WalletTransactions table
            _command.CommandText = @"INSERT INTO WalletTransactions (amount, date_transaction, type) 
                             VALUES (100.00, '2023-01-01 00:00:00', 'Credit')";
            _command.ExecuteNonQuery();

            // Insert data into the Client table
            _command.CommandText = @"INSERT INTO Client (User_id, cpf, WalletTransactions_id) 
                             VALUES (1, '123.456.789-09', 1)";
            _command.ExecuteNonQuery();

            // Insert data into the Product table
            _command.CommandText = @"INSERT INTO Product (name, description) 
                             VALUES ('Product A', 'Description for Product A')";
            _command.ExecuteNonQuery();

            // Insert data into the Selling table
            _command.CommandText = @"INSERT INTO Selling (date_sale, amount, Client_id, date_EndSale) 
                             VALUES ('2023-01-15', 50.00, 1, '2023-01-16 12:00:00')";
            _command.ExecuteNonQuery();

            // Insert data into the SellingItem table
            _command.CommandText = @"INSERT INTO SellingItem (Selling_id, Product_id, quantity, price, Company_id) 
                             VALUES (1, 1, 2, 25.00, 1)";
            _command.ExecuteNonQuery();

            // Insert data into the ProductOffer table
            _command.CommandText = @"INSERT INTO ProductOffer (Company_id, Product_id, price, quantity) 
                             VALUES (1, 1, 20.00, 10)";
            _command.ExecuteNonQuery();

            // Insert data into the ReviewSellingItem table
            _command.CommandText = @"INSERT INTO ReviewSellingItem (note, comment, SellingItem_Selling_id, SellingItem_Product_id) 
                             VALUES (5, 'Good product', 1, 1)";
            _command.ExecuteNonQuery();

            // Insert data into the ReviewCompany table
            _command.CommandText = @"INSERT INTO ReviewCompany (Client_id, Company_id, note, comment) 
                             VALUES (1, 1, 4, 'Great company')";
            _command.ExecuteNonQuery();

            // Insert data into the Refund table
            _command.CommandText = @"INSERT INTO Refund (date_opening, date_closure, status, isAccepted, SellingItem_Selling_id, SellingItem_Product_id) 
                             VALUES ('2023-01-20 10:00:00', '2023-01-25 15:00:00', 'Open', NULL, 1, 1)";
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
