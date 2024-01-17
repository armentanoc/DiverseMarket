using DiverseMarket.Backend.Infrastructure.Operations;
using DiverseMarket.Backend.Model.Enums;
using DiverseMarket.Backend.Model.Products;
using DiverseMarket.Backend.Model.Transactions;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace DiverseMarket.Backend.Infrastructure.Repositories
{
    internal class SellingItemDB : DatabaseConnection
    {
        internal static List<SellingItem> GetAllItemsBySellingId(long selling_id)
        {
            List<SellingItem> items = new List<SellingItem>();
            try
            {
                Open();
                string query = @"SELECT id, product_offer_id, quantity, unity_price, status where selling_id = @selling_id;";

                _command = new SQLiteCommand(query, _connection);
                _command.Parameters.AddWithValue("@selling_id", selling_id);
                var reader = _command.ExecuteReader();

                while (reader.Read())
                {
                    items.Add(new SellingItem((long)reader["id"], (long)reader["product_offer_id"], selling_id, (int)(long)reader["quantity"], 
                        (double)reader["unity_price"], (OrderStatus)(long)reader["status"]));
                }

                return items;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured: " + ex.Message);
                return items;

            }
            finally { Close(); }
        }

        internal static long GetCompanyIdBySellingItemId(long sellingItemId)
        {
            long id = 0;
            try
            {
                Open();
                string query = @"SELECT po.Company_id
                        FROM SellingItem si
                        JOIN ProductOffer po ON si.product_offer_id = po.id
                        WHERE si.id = @id;";
                _command = new SQLiteCommand(query, _connection);

                _command.Parameters.AddWithValue("@id", sellingItemId);

                var reader = _command.ExecuteReader();

                if (reader.Read())
                {
                    id = (long)reader[0];
                }

                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured: " + ex.Message);
                return id;

            }
            finally { Close(); }
        }

        internal static OrderStatus GetOrderItemStatusById(long orderItemId)
        {
            try
            {
                Open();
                string query = @"SELECT status FROM SellingItem
                                WHERE id = @id;";
                _command = new SQLiteCommand(query, _connection);

                _command.Parameters.AddWithValue("@id", orderItemId);

                var reader = _command.ExecuteReader();

                if(reader.Read())
                    return (OrderStatus) (long) reader[0];
                return OrderStatus.Preparation;

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured: " + ex.Message);
                return OrderStatus.Preparation;

            }
            finally { Close(); }
        }

        internal static string InitializeTable()
        {
            return @"
            CREATE TABLE IF NOT EXISTS SellingItem (
                id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                selling_id INTEGER NOT NULL,
                product_offer_id INTEGER NOT NULL,
                quantity INTEGER NOT NULL,
                unity_price DECIMAL(10,2) NOT NULL,
                status INTEGER NOT NULL CHECK(Status IN (1, 2, 3, 4)),
                FOREIGN KEY (selling_id) REFERENCES Selling(id) ON DELETE NO ACTION ON UPDATE NO ACTION,
                FOREIGN KEY (product_id) REFERENCES Product(id) ON DELETE NO ACTION ON UPDATE NO ACTION
            );";
        }

        internal static bool SetOrdemItemAsRecieved(long itemId)
        {
            try
            {
                Open();
                string query = @"UPDATE SellingItem
                            SET status = ?
                            WHERE id = @id;";
                _command = new SQLiteCommand(query, _connection);

                _command.Parameters.AddWithValue("@id", itemId);

                return _command.ExecuteNonQuery() > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured: " + ex.Message);
                return false;

            }
            finally { Close(); }
        }
    }
}
