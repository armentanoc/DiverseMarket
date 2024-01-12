using DiverseMarket.Backend.Model.Transactions;
using DiverseMarket.Backend.Model;
using DiverseMarket.Backend.Model.Products;

namespace DiverseMarket.Backend.Model.Transactions.Reviews
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
