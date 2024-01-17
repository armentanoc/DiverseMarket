using DiverseMarket.Backend.Infrastructure.Operations;
using DiverseMarket.Backend.Model;
using System.Data.SQLite;

namespace DiverseMarket.Backend.Infrastructure.Repositories
{
    public class ProductDB : DatabaseConnection
    {
        public static List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            try
            {
                Open();
                    string query = @"SELECT p.id, p.name, p.description, pc.name AS category_name
                                         FROM Product p
                                         JOIN ProductCategory pc ON p.ProductCategory_id = pc.id;";

                    using (var command = new SQLiteCommand(query, _connection))
                    {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product((long)reader["id"], reader["name"].ToString(),
                                reader["description"].ToString(), reader["category_name"].ToString()));
                        }
                    }
                }
                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return products;
            }
            finally
            {
                Close();
            }
        }

        internal static string InitializeTable()
        {
            return @"
                CREATE TABLE IF NOT EXISTS Product (
                    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    name VARCHAR(45) NOT NULL,
                    description VARCHAR(45) NOT NULL,
                    ProductCategory_id INTEGER NOT NULL,  
                    FOREIGN KEY (ProductCategory_id) REFERENCES ProductCategory(id) ON DELETE NO ACTION ON UPDATE NO ACTION
                ); insert into Product (name, description, ProductCategory_id) values ('Camiseta', 'camiseta nike', 1);";
        }
    }
}
