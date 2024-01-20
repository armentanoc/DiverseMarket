using DiverseMarket.Backend.Infrastructure.Operations;
using DiverseMarket.Backend.DTOs;
using DiverseMarket.Backend.Model.Enums;
using DiverseMarket.Backend.Model;
using System.Data.SQLite;
using DiverseMarket.Backend.Model.Transactions;
using System.Collections.Generic;
using System.Globalization;

namespace DiverseMarket.Backend.Infrastructure.Repositories
{
    internal class SellingDB : DatabaseConnection
    {
        internal static string InitializeTable()
        {
            return @"
            CREATE TABLE IF NOT EXISTS Selling (
                id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                date_sale DATE NOT NULL,
                amount DECIMAL(10,2) NOT NULL,
                Customer_id INTEGER NOT NULL,
                Company_id INTEGER NOT NULL,
                FOREIGN KEY (Customer_id) REFERENCES Customer(id) ON DELETE NO ACTION ON UPDATE NO ACTION,
                FOREIGN KEY (Company_id) REFERENCES Company(id) ON DELETE NO ACTION ON UPDATE NO ACTION
            );
            INSERT INTO Selling(date_sale, amount, Customer_id, Company_id)
            VALUES('2024-01-14', 100, 1, 1);
            INSERT INTO Selling(date_sale, amount, Customer_id, Company_id)
            VALUES('2024-01-13', 50, 2, 1);";
        }

        public static List<OrderBasicInfoDTO> GetAllOrdersByCompanyUserId(long userId)
        {
            //testing
            //INSERT INTO Selling(date_sale, amount, Customer_id, Company_id)
            //VALUES("14/01/2024", 100, 1, 1);
            //INSERT INTO Selling(date_sale, amount, Customer_id, Company_id)
            //VALUES("13/01/2024", 50, 2, 1);

            List<OrderBasicInfoDTO> orderList = new List<OrderBasicInfoDTO>();

            try
            {
                Open();
                //string query = "SELECT * FROM Selling WHERE Company_id = @id;";
                //_command = new SQLiteCommand(query, _connection);
                //_command.Parameters.AddWithValue("@id", userId);
                //var reader = _command.ExecuteReader();


                string query = @"
                    SELECT s.*
                    FROM Selling s
                    INNER JOIN Company c ON s.Company_id = c.id
                    WHERE c.User_id = @userId;";

                _command = new SQLiteCommand(query, _connection);
                _command.Parameters.AddWithValue("@userId", userId);

                var reader = _command.ExecuteReader();

                while (reader.Read())
                {
                    long id = (long)reader["id"];
                    DateTime date = DateTime.ParseExact(reader.GetString(reader.GetOrdinal("date_sale")), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    decimal amount = reader.GetDecimal(reader.GetOrdinal("amount"));
                    long customerId = (long)reader["Customer_id"];
                    long companyId = (long)reader["Company_id"];

                    var orderInfo = new OrderBasicInfoDTO(id, date, amount, customerId, companyId);
                    orderList.Add(orderInfo);
                }

                return orderList;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return null;
            }
            finally
            {
                Close();
            }
        }

        public static bool AddOrder(DateTime dateSale, decimal amount, long customerId, long companyId)
        {
            try
            {
                Open();

                string query = @"
                INSERT INTO Selling(date_sale, amount, Customer_id, Company_id)
                VALUES(@DateSale, @Amount, @CustomerId, @CompanyId);";

                _command = new SQLiteCommand(query, _connection);

                _command.Parameters.AddWithValue("@DateSale", dateSale);
                _command.Parameters.AddWithValue("@Amount", amount);
                _command.Parameters.AddWithValue("@CustomerId", customerId);
                _command.Parameters.AddWithValue("@CompanyId", companyId);

                int rowsAffected = _command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                MyLogger.Log.Error("An error occurred in AddSelling: " + ex.Message + ex.StackTrace);
                return false;
            }
            finally
            {
                Close();
            }
        }

        public static bool RemoveSelling(long sellingId)
        {
            try
            {
                Open();

                string query = @"DELETE FROM Selling WHERE id = @SellingId;";

                _command = new SQLiteCommand(query, _connection);
                _command.Parameters.AddWithValue("@SellingId", sellingId);

                int rowsAffected = _command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                MyLogger.Log.Error("An error occurred in RemoveSelling: " + ex.Message + ex.StackTrace);
                return false;
            }
            finally
            {
                Close();
            }
        }
    }
}
