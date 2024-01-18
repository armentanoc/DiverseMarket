using DiverseMarket.Backend.DTOs;
using DiverseMarket.Backend.Infrastructure.Operations;
using DiverseMarket.Backend.Model;
using DiverseMarket.Backend.Model.Enums;
using DiverseMarket.Backend.Model.Products;
using DiverseMarket.Backend.Model.Transactions;
using DiverseMarket.Logger;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        internal static DateTime GetOrderDateById(long orderId)
        {
            DateTime date = default;
            try
            {
                Open();
                string query = @"SELECT date_sale where id = @id;";

                _command = new SQLiteCommand(query, _connection);
                _command.Parameters.AddWithValue("@id", orderId);
                var reader = _command.ExecuteReader();

                if (reader.Read())
                {
                    try { return DateTime.Parse(reader["date_sale"].ToString()); }
                    catch { return date; }
                }

                return date;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured: " + ex.Message);
                return date;

            }
            finally { Close(); }
        }

        internal static Selling GetSellingById(long id)
        {
            Selling selling = null;
            try
            {
                Open();
                string query = @"SELECT id, date_sale, amount, Customer_id, status where id = @customerId;";

                _command = new SQLiteCommand(query, _connection);
                _command.Parameters.AddWithValue("@id", id);
                var reader = _command.ExecuteReader();

                if (reader.Read())
                {
                    return new Selling((long)reader["id"], (long)reader["Customer_id"], DateTime.Parse(reader["date_sale"].ToString()), 
                        (OrderStatus)(long)reader["status"], (double)reader["amount"]);
                }

                return selling;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured: " + ex.Message);
                return selling;

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
                status INTEGER NOT NULL CHECK(Status IN (1, 2, 3, 4, 5)),
                FOREIGN KEY (Customer_id) REFERENCES User(id) ON DELETE NO ACTION ON UPDATE NO ACTION
            );";
        }

        public static List<OrderBasicInfoDTO> GetAllOrdersByCompanyUserId(long userId)
        {
            List<OrderBasicInfoDTO> orderList = new List<OrderBasicInfoDTO>();

            try
            {
                Open();
                string query = "SELECT * FROM Selling WHERE Company_id = @id;";
                _command = new SQLiteCommand(query, _connection);
                _command.Parameters.AddWithValue("@id", userId);
                var reader = _command.ExecuteReader();

                while (reader.Read())
                {
                    long id = (long)reader["id"];
                    DateTime date = reader.GetDateTime(reader.GetOrdinal("date_sale"));
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
                new LogMessage($"An error occurred in {nameof(GetAllOrdersByCompanyUserId)} {ex.Message}");
                return null;
            }
            finally
            {
                Close();
            }
        }

    }
}
