namespace DiverseMarket.Backend.DTOs
{
    public class ProductOfferBasicInfoDTO
    {
        public long Id { get; }
        public long CompanyId { get; }
        public long ProductId { get; }
        public decimal Price { get; private set; }
        public long Quantity { get; private set; }

        public ProductOfferBasicInfoDTO(long id, long companyId, long productId, decimal price, long quantity)
        {
            Id = id;
            CompanyId = companyId;
            ProductId = productId;
            Price = price;
            Quantity = quantity;
        }
    }
}