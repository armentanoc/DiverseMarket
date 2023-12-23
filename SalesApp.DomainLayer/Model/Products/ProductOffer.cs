using SalesApp.DomainLayer.Model.Users;

namespace SalesApp.DomainLayer.Model.Products
{
    public class ProductOffer
    {
        public decimal Price { get; internal set; }
        public Seller Seller { get; internal set; }
    }
}
