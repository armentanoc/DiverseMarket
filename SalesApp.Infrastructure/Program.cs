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

                    Console.WriteLine($"\nPara abrir o banco no terminal: sqlite3 {AppDomain.CurrentDomain.BaseDirectory}bancotemporario.db\n");
                    //to check tables locally

                    //DisplayAllTableSchemas();
                    // Display table schemas

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

        private static void DisplayAllTableSchemas()
        {
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
