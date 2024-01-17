namespace DiverseMarket.Backend.DTOs
{
    public class ProductOfferBasicInfoDTO
    {
        public long Id { get; }
        public long CompanyUserId { get; }
        public long ProductId { get; }
        public decimal Price { get; private set; }
        public long Quantity { get; private set; }

        public ProductOfferBasicInfoDTO(long id, long companyUserId, long productId, decimal price, long quantity)
        {
            Id = id;
            CompanyUserId = companyUserId;
            ProductId = productId;
            Price = price;
            Quantity = quantity;
        }

        public ProductOfferBasicInfoDTO(long companyUserId, long productId, decimal price, long quantity)
        {
            CompanyUserId = companyUserId;
            ProductId = productId;
            Price = price;
            Quantity = quantity;
        }
    }
}