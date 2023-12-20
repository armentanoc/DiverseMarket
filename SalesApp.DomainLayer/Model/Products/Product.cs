namespace SalesApp.DomainLayer.Model.Products {

    using SalesApp.DomainLayer.Model.Users;

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
            this.Id = SetId();
            this.Name = name;
            this.Description = description;

        }

        private int SetId()
        {
            return Math.Abs(Guid.NewGuid().GetHashCode());
        }

        internal void AddReview(decimal review)
        {
            this.AverageRating = review + (this.NumberOfReviews * this.AverageRating);
            this.NumberOfReviews++;
            this.AverageRating /= this.NumberOfReviews;
        }

        internal bool ChangeDescription(string newDescription)
        {
            if(this.Description == newDescription || newDescription.Equals("")) return false;
            this.Description = newDescription; return true;
        }
    }
}
