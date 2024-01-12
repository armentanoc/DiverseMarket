using DiverseMarket.Backend.Infrastructure.Operations;
using System.Runtime.CompilerServices;

namespace DiverseMarket.Backend

{
    public class Program
    {
        public static void Main()
        {
            try
            {
                if (DatabaseConnection.Open())
                {
                    DatabaseConnection.CreateTables();
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
