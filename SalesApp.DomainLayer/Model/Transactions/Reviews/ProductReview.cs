using SalesApp.DomainLayer.Model.Products;

namespace SalesApp.DomainLayer.Model.Transactions.Reviews
{
    internal class ProductReview : Review
    {
        public Selling Selling { get; private set; }
        public ProductGeneral Product { get; private set; }
        public ProductReview(ReviewEnum reviewEnum, ProductGeneral product, Selling selling, string? comment = null) : base(reviewEnum, comment)
        {
            Product = product;
            Selling = selling;
        }
    }
}
