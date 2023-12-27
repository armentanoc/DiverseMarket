using SalesApp.Infrastructure.Operations;
using System.Runtime.CompilerServices;

namespace SalesApp.Infrastructure

{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (DatabaseConnection.Open())
                {
                    // Perform other operations or queries here

                    // Display table schemas
                    DatabaseConnection.DisplayTableSchema("Address");
                    DatabaseConnection.DisplayTableSchema("User");
                    DatabaseConnection.DisplayTableSchema("Product");
                    DatabaseConnection.DisplayTableSchema("ProductReview");

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
