namespace SalesApp.Backend.Model.Products
{

    using SalesApp.Backend.Model.Users;

    internal class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal AverageRating { get; private set; }
        public decimal NumberOfReviews { get; private set; }
        //implementar imagem?

        internal Product(string name, string description)
        {
            Id = SetId();
            Name = name;
            Description = description;

        }

        private int SetId()
        {
            return Math.Abs(Guid.NewGuid().GetHashCode());
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
