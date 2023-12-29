using SalesApp.DomainLayer.Model.Products;
using SalesApp.DomainLayer.Model.Transactions.Sellings.Selling;


namespace SalesApp.DomainLayer.Model.Transactions.Reviews
{
    internal class ProductReview : Review
    {
        public Selling Selling;
        public Product Product { get; private set; }
        public ProductReview(ReviewEnum reviewEnum, Product product, Selling selling, string? comment = null) : base(reviewEnum, comment)
        {
            Product = product;
            Selling = selling;
        }
    }
}
