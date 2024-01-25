namespace DiverseMarket.Backend.Model.Products
{

    using DiverseMarket.Backend.Model.Users;

    internal class Product
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Category { get; private set; }
        public decimal AverageRating { get; private set; }
        public decimal NumberOfReviews { get; private set; }
        //implementar imagem?

        public Product(long id, string name, string description, string category)
        {
            Id = id;
            Name = name;
            Description = description;
            Category = category;
        }

        internal void AddReview(decimal review)
        {
            AverageRating = review + NumberOfReviews * AverageRating;
            NumberOfReviews++;
            AverageRating /= NumberOfReviews;
        }

        internal bool ChangeDescription(string newDescription)
        {
            if (Description == newDescription || newDescription.Equals("")) return false;
            Description = newDescription; return true;
        }
    }
}
