using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesApp.DomainLayer.Model.Transactions;
using SalesApp.Infrastructure.Operations;

namespace SalesApp.Infrastructure.Repositories
{
    internal class SellingDB : DatabaseConnection
    {
        internal static string InitializeTable()
        {
            return @"CREATE TABLE IF NOT EXISTS Selling (
                            id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                            date_sale DATE NOT NULL,
                            amount DECIMAL(10,2),
                            Client_id INTEGER NOT NULL,
                            date_EndSale DATETIME,
                            FOREIGN KEY (Client_id) REFERENCES Client (id)
                                ON DELETE NO ACTION
                                ON UPDATE NO ACTION
                        );";

        }

        public SellingDB() : base()
        {
            Open();
        }

        public void AddSelling(Selling selling)
        {
            using (var transaction = _connection.BeginTransaction())
            {
                try
                {
                    using (var command = _connection.CreateCommand())
                    {
                        // Insert Selling
                        command.CommandText = "INSERT INTO Selling (date_sale, amount, Client_id, date_EndSale) " +
                                            "VALUES (@date_sale, @amount, @Client_id, @date_EndSale);";
                        command.Parameters.AddWithValue("@date_sale", selling.SaleStartDate);
                        command.Parameters.AddWithValue("@amount", selling.TotalValue);
                        command.Parameters.AddWithValue("@Client_id", selling.CustomerId);
                        command.Parameters.AddWithValue("@date_EndSale", selling.SaleEndDate);

                        command.ExecuteNonQuery();
                        command.CommandText = "SELECT last_insert_rowid();";
                        selling.Id = Convert.ToInt32(command.ExecuteScalar());
                    }

                    // Insert SellingItems
                    foreach (var sellingItem in selling.SellingItems)
                    {
                        using (var command = _connection.CreateCommand())
                        {
                            command.CommandText = "INSERT INTO SellingItem (Selling_id, Product_id, quantity, price, Company_id) " +
                                              "VALUES (@Selling_id, @Product_id, @quantity, @price, @Company_id);";
                            command.Parameters.AddWithValue("@Selling_id", selling.Id);
                            command.Parameters.AddWithValue("@Product_id", sellingItem.ProductId);
                            command.Parameters.AddWithValue("@quantity", sellingItem.Quantity);
                            command.Parameters.AddWithValue("@price", sellingItem.Price);
                            command.Parameters.AddWithValue("@Company_id", sellingItem.CompanyId);

                            command.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public Selling GetSellingById(int sellingId)
        {
            Selling selling = null;
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Selling WHERE id = @sellingId";
                command.Parameters.AddWithValue("@sellingId", sellingId);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        selling = MapSellingFromReader(reader);
                        selling.SellingItems = GetSellingItemsBySellingId(sellingId);
                    }
                }
            }

            return selling;
        }

        public List<SellingItem> GetSellingItemsBySellingId(int sellingId)
        {
            var sellingItems = new List<SellingItem>();

            using (var command = _connection.CreateCommand())  
            {
                command.CommandText = "SELECT * FROM SellingItem WHERE Selling_id = @Selling_id;";
                command.Parameters.AddWithValue("@Selling_id", sellingId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sellingItems.Add(MapSellingItemFromReader(reader));
                    }
                }
            }

            return sellingItems;
        }


        public void UpdateSelling(Selling selling)
        {
            using (var transaction = _connection.BeginTransaction())
            {
                try
                {
                    using (var command = _connection.CreateCommand())
                    {
                        // Update Selling
                        command.CommandText = "UPDATE Selling SET date_sale = @date_sale, amount = @amount, " +
                                            "Client_id = @Client_id, date_EndSale = @date_EndSale WHERE id = @id;";
                        command.Parameters.AddWithValue("@date_sale", selling.SaleStartDate);
                        command.Parameters.AddWithValue("@amount", selling.TotalValue);
                        command.Parameters.AddWithValue("@Client_id", selling.CustomerId);
                        command.Parameters.AddWithValue("@date_EndSale", selling.SaleEndDate);
                        command.Parameters.AddWithValue("@id", selling.Id);

                        command.ExecuteNonQuery();

                        // Delete existing SellingItems
                        command.CommandText = "DELETE FROM SellingItem WHERE Selling_id = @Selling_id;";
                        command.Parameters.AddWithValue("@Selling_id", selling.Id);
                        command.ExecuteNonQuery();
                    }
                    // Insert updated SellingItems
                    foreach (var sellingItem in selling.SellingItems)
                    {
                        using (var command = _connection.CreateCommand())
                        {
                            command.CommandText = "INSERT INTO SellingItem (Selling_id, Product_id, quantity, price, Company_id) " +
                                              "VALUES (@Selling_id, @Product_id, @quantity, @price, @Company_id);";
                            command.Parameters.AddWithValue("@Selling_id", selling.Id);
                            command.Parameters.AddWithValue("@Product_id", sellingItem.ProductId);
                            command.Parameters.AddWithValue("@quantity", sellingItem.Quantity);
                            command.Parameters.AddWithValue("@price", sellingItem.Price);
                            command.Parameters.AddWithValue("@Company_id", sellingItem.CompanyId);

                            command.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("erro no update " + ex);
                }
            }
        }

        public List<Selling> GetSellingsByCustomerId(int customerId)
        {
            var sellings = new List<Selling>();

            using (var transaction = _connection.BeginTransaction())
            {
                try
                {
                    using (var command = _connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM Selling WHERE Client_id = @Client_id;";
                        command.Parameters.AddWithValue("@Client_id", customerId);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var selling = MapSellingFromReader(reader);
                                selling.SellingItems = GetSellingItemsBySellingId((int)selling.Id);
                                sellings.Add(selling);
                            }
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return sellings;
        }

        private Selling MapSellingFromReader(IDataReader reader)
        {
            return new Selling
            {
                Id = Convert.ToInt32(reader["id"]),
                SaleStartDate = Convert.ToDateTime(reader["date_sale"]),
                SaleEndDate = reader["date_EndSale"] == DBNull.Value ? null : (DateTime?)reader["date_EndSale"],
                TotalValue = Convert.ToDecimal(reader["amount"]),
                CustomerId = Convert.ToInt32(reader["Client_id"]),
            };
        }

        private SellingItem MapSellingItemFromReader(IDataReader reader)
        {
            int quantity = Convert.ToInt32(reader["quantity"]);
            decimal price = Convert.ToDecimal(reader["price"]);
            int product = Convert.ToInt32(reader["Product_id"]);
            int company = Convert.ToInt32(reader["Company_id"]);
            SellingItem sellingItem = new SellingItem(product, company, quantity, price);
            return sellingItem;

        }
    }
}

