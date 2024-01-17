using DiverseMarket.Backend.Infrastructure.Operations;
using DiverseMarket.Backend.DTOs;
using System.Data.SQLite;
using DiverseMarket.Logger;

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
