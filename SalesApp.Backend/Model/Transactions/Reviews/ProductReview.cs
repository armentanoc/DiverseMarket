using SalesApp.Backend.Model.Transactions;
using SalesApp.Backend.Model.Products;

namespace SalesApp.Backend.Model.Transactions.Reviews
{
    internal class ProductReview : Review
    {
        public Selling Selling { get; private set; }
        public Product Product { get; private set; }
        public ProductReview(ReviewEnum reviewEnum, Product product, Selling selling, string? comment = null) : base(reviewEnum, comment)
        {
            Product = product;
            Selling = selling;
        }
    }
}
