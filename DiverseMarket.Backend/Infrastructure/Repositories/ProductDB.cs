using DiverseMarket.Backend.Infrastructure.Operations;
using DiverseMarket.Backend.Model;
using DiverseMarket.Logger;
using System.Data.SQLite;

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
        public static bool InsertProduct(Product product)
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

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
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

        public static void RegisterDefaultProducts()
        {
            try
            {
                Product product;

                product = new Product("Camisa Nike", "Preta", 1);
                InsertProduct(product);
                product = new Product("Camisa Adidas", "Azul", 1);
                InsertProduct(product);
                product = new Product("Camisa Oxer", "Cinza", 1);
                InsertProduct(product);
                product = new Product("Tênis Olympikus", "Preto", 2);
                InsertProduct(product);
                product = new Product("Tênis Nike", "Vermelho", 2);
                InsertProduct(product);
                product = new Product("Tênis Adidas", "Branco", 2);
                InsertProduct(product);
                product = new Product("Macbook Air", "Prata", 3);
                InsertProduct(product);
                product = new Product("Monitor Concórdia", "Preto e Vermelho", 3);
                InsertProduct(product);
                product = new Product("Mouse Logitech", "Branco", 3);
                InsertProduct(product);
                product = new Product("Clean Code", "Autor: Robert C. Martin", 4);
                InsertProduct(product);
                product = new Product("Domain-driven design", "Autor: Eric Evans", 4);
                InsertProduct(product);
                product = new Product("Data Science Do Zero", "Autor: Joel Grus", 4);
                InsertProduct(product);
                product = new Product("Colar Lua", "Prata 925", 5);
                InsertProduct(product);
                product = new Product("Aliança Sol", "Ouro 18K", 5);
                InsertProduct(product);
                product = new Product("Brinco Terra", "Prata 925", 5);
                InsertProduct(product);
                product = new Product("Brinco Mar", "Prata 925", 5);
                InsertProduct(product);
                product = new Product("Brinco Céu", "Prata 925", 5);
                InsertProduct(product);
            }
            catch (Exception ex)
            {
                new LogMessage(ex);
            }   
        }
    }
}
