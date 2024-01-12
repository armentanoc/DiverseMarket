using SalesApp.Backend.Infrastructure.Operations;
using System.Runtime.CompilerServices;

namespace SalesApp.Backend

{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (DatabaseConnection.Open())
                {
                    // Creates tables
                    DatabaseConnection.CreateTables();

                    Console.WriteLine($"\nLocalização do banco: {Directory.GetCurrentDirectory()}");
                    //sqlite3 current directory to check tables

                    // Display table schemas
                    DatabaseConnection.DisplayTableSchema("Address");
                    DatabaseConnection.DisplayTableSchema("Company");
                    DatabaseConnection.DisplayTableSchema("Customer");
                    DatabaseConnection.DisplayTableSchema("ProductCategory");
                    DatabaseConnection.DisplayTableSchema("Product");
                    DatabaseConnection.DisplayTableSchema("ProductOffer");
                    DatabaseConnection.DisplayTableSchema("ProductReview");
                    DatabaseConnection.DisplayTableSchema("ReviewCompany");
                    DatabaseConnection.DisplayTableSchema("ReviewSellingItem");
                    DatabaseConnection.DisplayTableSchema("Selling");
                    DatabaseConnection.DisplayTableSchema("User");
                    DatabaseConnection.DisplayTableSchema("WalletTransactions");

                    // Close the connection
                    DatabaseConnection.Close();
                }
                else
                {
                    Console.WriteLine("Failed to open the database connection.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
