
using DiverseMarket.Backend.Model;
using DiverseMarket.Logger;

namespace DiverseMarket.Backend.Infrastructure.Repositories.DefaultData
{
    internal class DefaultProducts
    {
        public static Product[] GetProducts()
        {
            return
            [
                new Product("Camisa Nike", "Preta", 1),
                new Product("Camisa Adidas", "Azul", 1),
                new Product("Camisa Oxer", "Cinza", 1),
                new Product("Camisa Nike", "Cinza", 1),
                new Product("Camisa Adidas", "Cinza", 1),
                new Product("Camisa Oxer", "Amarela", 1),
                new Product("Tênis Olympikus", "Preto", 2),
                new Product("Tênis Nike", "Vermelho", 2),
                new Product("Tênis Adidas", "Branco", 2),
                new Product("Tênis Adidas", "Preto", 2),
                new Product("Tênis Adidas", "Verde", 2),
                new Product("Macbook Air", "Prata", 3),
                new Product("Mouse Logitech", "Preto", 3),
                new Product("Apple Magic Mouse", "Branco", 3),
                new Product("Monitor Concórdia", "Preto e Vermelho", 3),
                new Product("Monitor Dell", "Preto", 3),
                new Product("Monitor Samsung 27'", "Preto", 3),
                new Product("Mouse Logitech", "Branco", 3),
                new Product("Clean Code", "Autor: Robert C. Martin", 4),
                new Product("Domain-driven design", "Autor: Eric Evans", 4),
                new Product("Data Science Do Zero", "Autor: Joel Grus", 4),
                new Product("Colar Lua", "Prata 925", 5),
                new Product("Aliança Sol", "Ouro 18K", 5),
                new Product("Brinco Terra", "Prata 925", 5),
                new Product("Brinco Mar", "Prata 925", 5),
                new Product("Brinco Céu", "Prata 925", 5)
            ];
        }
    public static void Insert()
        {
            try
            {
                GetProducts()
                    .ToList()
                    .ForEach(product => ProductDB.InsertProduct(product));
            }
            catch (Exception ex)
            {
                new LogMessage(ex);
            }
        }
    }
}
