
namespace DiverseMarket.Backend.Model
{
    public class Product
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Category { get; private set; }
        public long CategoryId { get; private set; }

        public Product(long id, string name, string description, string category)
        {
            Id = id;
            Name = name;
            Description = description;
            Category = category;
        }

        public Product(string name, string description, long categoryId)
        {
            Name = name;
            Description = description;
            CategoryId = categoryId;
        }
    }
}
