using DiverseMarket.Backend.Infrastructure.Operations;
using DiverseMarket.Backend.Model.Enums;
using DiverseMarket.Backend.Model.Transactions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiverseMarket.Backend.Infrastructure.Repositories
{
    internal class RefundDB : DatabaseConnection
    {
        internal static List<Refund> GetAllRefundsByCustomerId(long customerId)
        {
            List<Refund> refunds = new List<Refund>();
            try
            {
                Open();
                string query = @"SELECT Id, Product_id, Company_id, Status, TotalAmount, CustomerComment, 
                            ModeratorComment, SellerComment where Customer_id = @customerId;";

                _command = new SQLiteCommand(query, _connection);
                _command.Parameters.AddWithValue("@customerId", customerId);
                var reader = _command.ExecuteReader();

                while (reader.Read())
                {
                    refunds.Add(new Refund((long)reader["Id"], customerId, (long)reader["Product_id"],
                        (long)reader["Company_id"], reader["CustomerComment"].ToString(), reader["SellerComment"].ToString(),
                        reader["ModeratorComment"].ToString(), (RefundStatus)(long)reader["Status"], (double)reader["TotalAmount"]));
                }

                return refunds;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured: " + ex.Message);
                return refunds;

            }
            finally { Close(); }
        }

        internal static string InitializeTable()
        {
            return @"
            CREATE TABLE IF NOT EXISTS Refund (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Customer_id INTEGER NOT NULL,
                Product_id INTEGER NOT NULL,
                Company_id INTEGER NOT NULL,
                Status INTEGER NOT NULL CHECK(Status IN (1, 2, 3, 4)),
                TotalAmount REAL NOT NULL,
                CustomerComment TEXT,
                ModeratorComment TEXT,
                SellerComment TEXT,
                FOREIGN KEY (Customer_id) REFERENCES User(id) ON DELETE NO ACTION ON UPDATE NO ACTION,
                FOREIGN KEY (Product_id) REFERENCES Product(id) ON DELETE NO ACTION ON UPDATE NO ACTION,
                FOREIGN KEY (Company_id) REFERENCES Company(id) ON DELETE NO ACTION ON UPDATE NO ACTION
            );
            ";

        }
    }
}
