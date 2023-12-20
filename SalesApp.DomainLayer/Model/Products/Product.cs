

namespace SalesApp.DomainLayer.Model.Products
{
    using SalesApp.DomainLayer.Model.Users;

    internal class Product
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public decimal AverageRating { get; private set; }
        public decimal NumberOfReviews { get; private set; }
        public List<Seller> Sellers { get; private set; }
        //implementar imagem?

        internal Product(string name, string description, decimal price, Seller firstSeller)
        {
            this.Id = SetId();
            this.Name = name;
            this.Description = description;
            this.Price = price;
            Sellers = new List<Seller>();
            if (firstSeller != null ) { this.Sellers.Add(firstSeller); }

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

        internal bool AddSeller(Seller seller)
        {
            if (seller == null || this.Sellers.Contains(seller)) return false;

            this.Sellers.Add(seller);

            return true;
        }

        internal bool ChangePrice(decimal newPrice)
        {
            if(this.Price == 0 || newPrice <= 0) return false;

            this.Price = newPrice; return true;
        }

        internal bool ChangeDescription(string newDescription)
        {
            if(this.Description == newDescription || newDescription.Equals("")) return false;
            this.Description = newDescription;
        }
    }
}
