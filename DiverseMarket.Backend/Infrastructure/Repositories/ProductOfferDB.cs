using DiverseMarket.Backend.Infrastructure.Operations;
using DiverseMarket.Backend.Model;
using DiverseMarket.Backend.Model.Companies;
using DiverseMarket.Backend.Model.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

        internal static List<ProductOffer> GetAllCompanyProductOffers(long userId)
        {
            List<ProductOffer> productOffers = new();
            try
            {
                Open();

                string query = @"SELECT *
                                FROM ProductOffer
                                WHERE Company_id = @CompanyId;";

                _command = new SQLiteCommand(query, _connection);

                _command.Parameters.AddWithValue("@CompanyId", userId);

                var reader = _command.ExecuteReader();

                while (reader.Read())
                {
                    ProductOffer productOffer = new ProductOffer
                    (
                        id :  Convert.ToInt32(reader["id"]),
                        sellerId : Convert.ToInt32(reader["Company_id"]),
                        productId: Convert.ToInt32(reader["Product_id"]),
                        price : Convert.ToDecimal(reader["price"]),
                        quantity : Convert.ToInt32(reader["quantity"])
                    );

                    productOffers.Add(productOffer);
                }
                LogError("list size: " + productOffers.Count);
                LogError("user id: " + userId);
                return productOffers;
            }
            catch (Exception ex)
            {
                //Console.WriteLine("An error occured: " + ex.Message);
                LogError("GetAllCompanyProductOffers", ex);
                return productOffers;
            }
            finally 
            { 
                Close(); 
            }
        }
        private static void LogError(string methodName, Exception ex)
        {
            string logFilePath = "error_log.txt";
            try
            {
                using (StreamWriter sw = new StreamWriter(logFilePath, true))
                {
                    sw.WriteLine($"{DateTime.Now}: Error in method {methodName}: {ex.Message}");
                }
            }
            catch (IOException)
            {
                // Handle exception, e.g., log to console
                Console.WriteLine($"Failed to write to log file: {ex.Message}");
            }
        }
        private static void LogError(string log)
        {
            string logFilePath = "error_log.txt";
            try
            {
                using (StreamWriter sw = new StreamWriter(logFilePath, true))
                {
                    sw.WriteLine($"{DateTime.Now}: {log}");
                }
            }
            catch (IOException ex)
            {
                // Handle exception, e.g., log to console
                Console.WriteLine($"Failed to write to log file: {ex.Message}");
            }
        }
        internal static bool RegisterDefaultProductOffer()
        {
            try
            {
                Open();

                string query = @"INSERT INTO ProductOffer(Company_id, Product_id, price, quantity)
                VALUES(2, 1, 99, 5);";

                _command = new SQLiteCommand(query, _connection);

                bool registered = _command.ExecuteNonQuery() > 0;

                return registered;
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
