using SalesApp.DomainLayer.Model.Products;

namespace SalesApp.DomainLayer.Model.Transactions.Reviews
{
    public class ProductReview : Review
    {
        private Selling Selling { get; set; }
        private Product Product { get; set; }
        private ProductReview(ReviewEnum reviewEnum, Product product, Selling selling, string? comment = null) : base(reviewEnum, comment)
        {
            Product = product;
            Selling = selling;
        }
    }
}
