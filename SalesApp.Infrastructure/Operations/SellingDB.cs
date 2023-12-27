using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using SalesApp.DomainLayer.Model.Transactions;
using SalesApp.DomainLayer.Model.Companies;
using SalesApp.DomainLayer.Model.Products;
using SalesApp.DomainLayer.Model.Users;
using System.Diagnostics;

namespace SalesApp.Infrastructure.Operations
{
    public class SellingDB : DatabaseConnection
    {
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
                    // Insert Selling
                    _command.CommandText = "INSERT INTO Selling (date_sale, amount, Client_id, date_EndSale) " +
                                            "VALUES (@date_sale, @amount, @Client_id, @date_EndSale);";
                    _command.Parameters.AddWithValue("@date_sale", selling.SaleStartDate);
                    _command.Parameters.AddWithValue("@amount", selling.TotalValue);
                    _command.Parameters.AddWithValue("@Client_id", selling.CustomerId);
                    _command.Parameters.AddWithValue("@date_EndSale", selling.SaleEndDate);

                    _command.ExecuteNonQuery();
                    _command.CommandText = "SELECT last_insert_rowid();";
                    selling.Id = Convert.ToInt32(_command.ExecuteScalar());

                    // Insert SellingItems
                    foreach (var sellingItem in selling.SellingItems)
                    {
                        _command.CommandText = "INSERT INTO SellingItem (Selling_id, Product_id, quantity, price, Company_id) " +
                                              "VALUES (@Selling_id, @Product_id, @quantity, @price, @Company_id);";
                        _command.Parameters.AddWithValue("@Selling_id", selling.Id);
                        _command.Parameters.AddWithValue("@Product_id", sellingItem.ProductId);
                        _command.Parameters.AddWithValue("@quantity", sellingItem.Quantity);
                        _command.Parameters.AddWithValue("@price", sellingItem.Price);
                        _command.Parameters.AddWithValue("@Company_id", sellingItem.CompanyId);

                        _command.ExecuteNonQuery();
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
            _command.CommandText = "SELECT * FROM Selling WHERE id = @id;";
            _command.Parameters.AddWithValue("@id", sellingId);

            using (var reader = _command.ExecuteReader())
            {
                if (reader.Read())
                {
                    var selling = MapSellingFromReader(reader);
                    selling.SellingItems = GetSellingItemsBySellingId(sellingId);
                    return selling;
                }
                return null;
            }
        }

        private List<SellingItem> GetSellingItemsBySellingId(int sellingId)
        {
            var sellingItems = new List<SellingItem>();
            _command.CommandText = "SELECT * FROM SellingItem WHERE Selling_id = @Selling_id;";
            _command.Parameters.AddWithValue("@Selling_id", sellingId);

            using (var reader = _command.ExecuteReader())
            {
                while (reader.Read())
                {
                  //  sellingItems.Add(MapSellingItemFromReader(reader));
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
                    // Update Selling
                    _command.CommandText = "UPDATE Selling SET date_sale = @date_sale, amount = @amount, " +
                                            "Client_id = @Client_id, date_EndSale = @date_EndSale WHERE id = @id;";
                    _command.Parameters.AddWithValue("@date_sale", selling.SaleStartDate);
                    _command.Parameters.AddWithValue("@amount", selling.TotalValue);
                    _command.Parameters.AddWithValue("@Client_id", selling.CustomerId);
                    _command.Parameters.AddWithValue("@date_EndSale", selling.SaleEndDate);
                    _command.Parameters.AddWithValue("@id", selling.Id);

                    _command.ExecuteNonQuery();

                    // Delete existing SellingItems
                    _command.CommandText = "DELETE FROM SellingItem WHERE Selling_id = @Selling_id;";
                    _command.Parameters.AddWithValue("@Selling_id", selling.Id);
                    _command.ExecuteNonQuery();

                    // Insert updated SellingItems
                    foreach (var sellingItem in selling.SellingItems)
                    {
                        _command.CommandText = "INSERT INTO SellingItem (Selling_id, Product_id, quantity, price, Company_id) " +
                                              "VALUES (@Selling_id, @Product_id, @quantity, @price, @Company_id);";
                        _command.Parameters.AddWithValue("@Selling_id", selling.Id);
                        _command.Parameters.AddWithValue("@Product_id", sellingItem.ProductId);
                        _command.Parameters.AddWithValue("@quantity", sellingItem.Quantity);
                        _command.Parameters.AddWithValue("@price", sellingItem.Price);
                        _command.Parameters.AddWithValue("@Company_id", sellingItem.CompanyId);

                        _command.ExecuteNonQuery();
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

