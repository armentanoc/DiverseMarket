using DiverseMarket.Backend.DTOs;
using DiverseMarket.Backend.Infrastructure.Operations;
using DiverseMarket.Logger;
using System.Data.SQLite;

namespace DiverseMarket.Backend.Infrastructure.Repositories
{
    internal class ReviewCompanyDB : DatabaseConnection
    {
        internal static string InitializeTable()
        {
            return @"
            CREATE TABLE IF NOT EXISTS ReviewCompany (
                id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                Customer_id INTEGER NOT NULL,
                Company_id INTEGER NOT NULL,
                review TEXT NOT NULL CHECK(review IN ('Pessimo', 'Ruim', 'Regular', 'Otimo', 'Excelente')),
                comment VARCHAR(150),
                FOREIGN KEY (Company_id) REFERENCES Company(id) ON DELETE SET NULL ON UPDATE CASCADE,
                FOREIGN KEY (Customer_id) REFERENCES Customer(id) ON DELETE SET NULL ON UPDATE CASCADE
            );";

        }

        public static void Insert(long clientId, long companyId, string review, string comment)
        {
            try
            {
                
                using (var command = new SQLiteCommand(_connection))
                {
                    command.CommandText = "INSERT INTO ReviewCompany (Client_id, Company_id, review, comment) VALUES (@ClientId, @CompanyId, @Review, @Comment);";

                    command.Parameters.AddWithValue("@ClientId", clientId) ;
                    command.Parameters.AddWithValue("@CompanyId", companyId);
                    command.Parameters.AddWithValue("@Review", review);
                    command.Parameters.AddWithValue("@Comment", comment);

                    _connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                new LogMessage(ex);
            }
        }
    }
}

