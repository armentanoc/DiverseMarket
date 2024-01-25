
namespace DiverseMarket.Backend.Model.Transactions.Reviews
{
    internal class CompanyReview : Review
    {

        public Selling Selling { get; private set; }
        public Product Product { get; private set; }
        public CompanyReview(ReviewEnum reviewEnum, Product product, Selling selling, string? comment = null) : base(reviewEnum, comment)
        {
            Product = product;
            Selling = selling;
        }
    }
}
