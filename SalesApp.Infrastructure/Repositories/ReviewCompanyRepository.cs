using SalesApp.Infrastructure.Operations;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.Infrastructure.Repositories
{
    public class ReviewCompanyRepository
    {
        internal static string InitializeTable()
        {
            return @"
            CREATE TABLE IF NOT EXISTS ReviewCompany (
                id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                Client_id INTEGER,
                Company_id INTEGER,
                review TEXT NOT NULL CHECK(review IN ('Pessimo', 'Ruim', 'Regular', 'Otimo', 'Excelente')),
                comment VARCHAR(45)
            );";
        }

        public static void AddCompanyReview(int clientId, int companyId, string review, string comment)
        {
            try
            {
                using (var connection = DatabaseConnection.Instance.Connection)
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "INSERT INTO ReviewCompany (Client_id, Company_id, review, comment) VALUES (@ClientId, @CompanyId, @Review, @Comment);";

                    command.Parameters.AddWithValue("@ClientId", clientId);
                    command.Parameters.AddWithValue("@CompanyId", companyId);
                    command.Parameters.AddWithValue("@Review", review);
                    command.Parameters.AddWithValue("@Comment", comment);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving CompanyReview: {ex.Message}");
            }
        }

    }
}
