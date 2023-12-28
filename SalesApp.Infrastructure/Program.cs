using SalesApp.Infrastructure.Operations;
using System;

namespace SalesApp.Infrastructure
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                if (DatabaseConnection.Instance.Open())
                {
                    // Creates tables
                    DatabaseConnection.Instance.CreateTables();

                    Console.WriteLine($"\nLocalização do banco: {AppDomain.CurrentDomain.BaseDirectory}");
                    // sqlite3 current directory to check tables

                    // Display table schemas
                    DisplayTableSchema("Address");
                    DisplayTableSchema("Company");
                    DisplayTableSchema("Customer");
                    DisplayTableSchema("ProductCategory");
                    DisplayTableSchema("Product");
                    DisplayTableSchema("ProductOffer");
                    DisplayTableSchema("ProductReview");
                    DisplayTableSchema("ReviewCompany");
                    DisplayTableSchema("ReviewSellingItem");
                    DisplayTableSchema("Selling");
                    DisplayTableSchema("User");
                    DisplayTableSchema("WalletTransactions");

                    // Close the connection
                    DatabaseConnection.Instance.Close();
                }
                else
                {
                    Console.WriteLine("Falha em abrire conexão com o banco de dados.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        private static void DisplayTableSchema(string tableName)
        {
            try
            {
                DatabaseConnection.Instance.DisplayTableSchema(tableName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro exibindo o schema da tabela {tableName}: {ex.Message}");
            }
        }
    }
}
