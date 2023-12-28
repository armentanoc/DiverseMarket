using System;
using System.Data.SQLite;

namespace SalesApp.Infrastructure.Operations
{
    public class SaveCompanyReview
    {
        public static void Execute(int clientId, int companyId, string review, string comment)
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
