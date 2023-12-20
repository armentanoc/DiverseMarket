using SalesApp.DomainLayer.Model.Users;

namespace SalesApp.DomainLayer.Model.Products
{
    public class ProductSeller
    {
        public decimal Price { get; internal set; }
        public Seller Seller { get; internal set; }
    }
}
