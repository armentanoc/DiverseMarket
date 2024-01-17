using DiverseMarket.Backend.Infrastructure.Operations;
using DiverseMarket.Backend.Model.Enums;
using DiverseMarket.Logger;
using System.Data.SQLite;

namespace DiverseMarket.Backend.Infrastructure.Repositories
{
    internal class ProductCategoryDB : DatabaseConnection
    {
        internal static string InitializeTable()
        {
            return @"
            CREATE TABLE IF NOT EXISTS ProductCategory (
                id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                name VARCHAR(45) NOT NULL
            );";

        }

        internal static void RegisterDefaultProductCategories()
        {
            var allCategories = Enum.GetValues<ProductCategory>().ToList();
            InsertDefaultProductCategories(allCategories);

        }

        internal static bool InsertDefaultProductCategories(IEnumerable<ProductCategory> categoryNames)
        {
            try
            {
                Open();

                using (var command = new SQLiteCommand(_connection))
                {
                    command.CommandText = "INSERT INTO ProductCategory (name) VALUES (@CategoryName)";

                    foreach (var categoryName in categoryNames)
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@CategoryName", categoryName.ToString());

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected <= 0)
                        {
                            return false; 
                        }
                    }
                    return true; 
                }
            }
            catch (Exception ex)
            {
                new LogMessage(ex);
                return false;
            }
            finally
            {
                Close();
            }
        }
    }
}
