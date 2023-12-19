using SalesApp.DomainLayer.Model.Products;

namespace SalesApp.DomainLayer.Model.Transactions.Reviews
{
    internal class ProductReview : Review
    {

        public ProductGeneral Product { get; private set; }
        public ProductReview(ReviewEnum reviewEnum, ProductGeneral product, string? comment = null) : base(reviewEnum, comment)
        {
            Product = product;
        }
    }
}
