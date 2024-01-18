using DiverseMarket.Backend.Infrastructure.Repositories;
using DiverseMarket.Backend.Model;
using DiverseMarket.Logger;

namespace DiverseMarket.Backend.Infrastructure.Repositories.DefaultData
{
    internal class DefaultProducts
    {
        public static void Insert()
        {
            try
            {
                Product product;

                product = new Product("Camisa Nike", "Preta", 1);
                ProductDB.InsertProduct(product);
                product = new Product("Camisa Adidas", "Azul", 1);
                ProductDB.InsertProduct(product);
                product = new Product("Camisa Oxer", "Cinza", 1);
                ProductDB.InsertProduct(product);
                product = new Product("Camisa Nike", "Cinza", 1);
                ProductDB.InsertProduct(product);
                product = new Product("Camisa Adidas", "Cinza", 1);
                ProductDB.InsertProduct(product);
                product = new Product("Camisa Oxer", "Amarela", 1);
                ProductDB.InsertProduct(product);
                product = new Product("Tênis Olympikus", "Preto", 2);
                ProductDB.InsertProduct(product);
                product = new Product("Tênis Nike", "Vermelho", 2);
                ProductDB.InsertProduct(product);
                product = new Product("Tênis Adidas", "Branco", 2);
                ProductDB.InsertProduct(product);
                product = new Product("Tênis Adidas", "Preto", 2);
                ProductDB.InsertProduct(product);
                product = new Product("Tênis Adidas", "Verde", 2);
                ProductDB.InsertProduct(product);
                product = new Product("Macbook Air", "Prata", 3);
                ProductDB.InsertProduct(product);
                product = new Product("Mouse Logitech", "Preto", 3);
                ProductDB.InsertProduct(product);
                product = new Product("Apple Magic Mouse", "Branco", 3);
                ProductDB.InsertProduct(product);
                product = new Product("Monitor Concórdia", "Preto e Vermelho", 3);
                ProductDB.InsertProduct(product);
                product = new Product("Monitor Dell", "Preto", 3);
                ProductDB.InsertProduct(product);
                product = new Product("Monitor Samsung 27'", "Preto", 3);
                ProductDB.InsertProduct(product);
                product = new Product("Mouse Logitech", "Branco", 3);
                ProductDB.InsertProduct(product);
                product = new Product("Clean Code", "Autor: Robert C. Martin", 4);
                ProductDB.InsertProduct(product);
                product = new Product("Domain-driven design", "Autor: Eric Evans", 4);
                ProductDB.InsertProduct(product);
                product = new Product("Data Science Do Zero", "Autor: Joel Grus", 4);
                ProductDB.InsertProduct(product);
                product = new Product("Colar Lua", "Prata 925", 5);
                ProductDB.InsertProduct(product);
                product = new Product("Aliança Sol", "Ouro 18K", 5);
                ProductDB.InsertProduct(product);
                product = new Product("Brinco Terra", "Prata 925", 5);
                ProductDB.InsertProduct(product);
                product = new Product("Brinco Mar", "Prata 925", 5);
                ProductDB.InsertProduct(product);
                product = new Product("Brinco Céu", "Prata 925", 5);
                ProductDB.InsertProduct(product);
            }
            catch (Exception ex)
            {
                new LogMessage(ex);
            }
        }
    }
}
