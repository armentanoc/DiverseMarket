using DiverseMarket.Backend.Infrastructure.Operations;
using DiverseMarket.Backend.Model;
using DiverseMarket.Logger;
using System.Data.SQLite;
using DiverseMarket.Backend.Infrastructure.Repositories.DefaultData;

namespace DiverseMarket.Backend.Infrastructure.Repositories
{
    public class ProductDB : DatabaseConnection
    {
        internal static string InitializeTable()
        {
            return @"
                CREATE TABLE IF NOT EXISTS Product (
                    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    name VARCHAR(45) NOT NULL,
                    description VARCHAR(45) NOT NULL,
                    ProductCategory_id INTEGER NOT NULL,  
                    FOREIGN KEY (ProductCategory_id) REFERENCES ProductCategory(id) ON DELETE NO ACTION ON UPDATE NO ACTION
                );";
        }
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
                new LogMessage(ex);
                return products;
            }
            finally
            {
                Close();
            }
        }
        public static int InsertProduct(Product product)
        {
            try
            {
                Open();

                using (var command = new SQLiteCommand(_connection))
                {
                    command.CommandText = @"INSERT INTO Product (name, description, ProductCategory_id) 
                                       VALUES (@Name, @Description, @ProductCategory_id)";

                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Description", product.Description);
                    command.Parameters.AddWithValue("@ProductCategory_id", product.CategoryId);
                    command.ExecuteNonQuery();
                }

                using (var selectCommand = new SQLiteCommand("SELECT last_insert_rowid();", _connection))
                {
                    int id = Convert.ToInt32(selectCommand.ExecuteScalar());
                    return id;
                }
            }
            catch (Exception ex)
            {
                new LogMessage(ex);
                return 0;
            }
            finally
            {
                Close();
            }
        }

        public static bool ProductExists(Product product)
        {
            try
            {
                Open();

                using (var command = new SQLiteCommand(_connection))
                {
                    command.CommandText = @"SELECT COUNT(*) FROM Product WHERE name = @Name AND description = @description";
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@description", product.Description);

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count > 0;
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



        public static void RegisterDefaultProducts() => DefaultProducts.Insert();
    }
}
