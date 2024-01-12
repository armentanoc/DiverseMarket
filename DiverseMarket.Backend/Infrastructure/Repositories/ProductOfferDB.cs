using DiverseMarket.Backend.Infrastructure.Operations;
using DiverseMarket.Backend.Model.Products;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiverseMarket.Backend.Infrastructure.Repositories
{
    public class ProductOfferDB : DatabaseConnection
    {
        public static string InitializeTable()
        {
            return @"
            CREATE TABLE IF NOT EXISTS ProductOffer (
                id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                Company_id INTEGER NOT NULL,
                Product_id INTEGER NOT NULL,
                price DECIMAL(10,2),
                quantity INTEGER,
                FOREIGN KEY (Company_id) REFERENCES Company(id) ON DELETE SET NULL ON UPDATE CASCADE,
                FOREIGN KEY (Product_id) REFERENCES Product(id) ON DELETE SET NULL ON UPDATE CASCADE
            );
            ";
        }


        public static double GetLowestPriceByProductId(long producId)
        {
            double price = 0;
            try
            {
                Open();
                string query = @"SELECT MIN(price) AS lowest_price
                                FROM ProductOffer
                                WHERE Product_id = @ProductId;";
                _command = new SQLiteCommand(query, _connection);

                _command.Parameters.AddWithValue("@ProductId", producId);

                var reader = _command.ExecuteReader();

                if (reader.Read())
                {
                    price = (double)reader[0];
                }

                return price;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured: " + ex.Message);
                return price;

            }
            finally { Close(); }
        }

        internal static long GetProductIdByProductOfferId(long productOfferId)
        {
            long id = 0;
            try
            {
                Open();
                string query = @"SELECT Product_id from ProductOffer
                                WHERE id = @id;";
                _command = new SQLiteCommand(query, _connection);

                _command.Parameters.AddWithValue("@id", productOfferId);

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
    }
}
