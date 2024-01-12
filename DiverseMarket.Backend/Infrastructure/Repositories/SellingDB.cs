using DiverseMarket.Backend.Infrastructure.Operations;
using DiverseMarket.Backend.Model;
using DiverseMarket.Backend.Model.Enums;
using DiverseMarket.Backend.Model.Products;
using DiverseMarket.Backend.Model.Transactions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiverseMarket.Backend.Infrastructure.Repositories
{
    internal class SellingDB : DatabaseConnection
    {
        internal static List<Selling> GetAllSellingByCustomerId(long customerId)
        {
            List<Selling> sellings = new List<Selling>();
            try
            {
                Open();
                string query = @"SELECT id, date_sale, amount, status where Customer_id = @customerId;";
                    
                _command = new SQLiteCommand(query, _connection);
                _command.Parameters.AddWithValue("@customerId", customerId);
                var reader = _command.ExecuteReader();

                while (reader.Read())
                {
                    sellings.Add(new Selling((long)reader["id"], customerId, DateTime.Parse(reader["date_sale"].ToString()), 
                        (OrderStatus)(long)reader["status"], (double)reader["amount"]));
                }

                return sellings;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured: " + ex.Message);
                return sellings;

            }
            finally { Close(); }
        }

        internal static string InitializeTable()
        {
            return @"
            CREATE TABLE IF NOT EXISTS Selling (
                id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                date_sale DATE NOT NULL,
                amount DECIMAL(10,2) NOT NULL,
                Customer_id INTEGER NOT NULL,
                status INTEGER NOT NULL CHECK(Status IN (1, 2, 3, 4)),
                FOREIGN KEY (Customer_id) REFERENCES User(id) ON DELETE NO ACTION ON UPDATE NO ACTION
            );";

        }
    }
}
