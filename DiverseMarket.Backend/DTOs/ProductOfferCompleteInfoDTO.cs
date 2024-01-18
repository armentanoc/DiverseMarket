namespace DiverseMarket.Backend.DTOs
{
    public class ProductOfferCompleteInfoDTO
    {
        public long Id { get; }
        public long CompanyId { get; }
        public long ProductId { get; }
        public string Name { get; }
        public string Description { get; }
        public string Category { get; }
        public decimal Price { get; }
        public long Quantity { get; }

        public ProductOfferCompleteInfoDTO(long id, long companyId, long productId, decimal price, long quantity, string name, string category, string description)
        {
            Id = id;
            CompanyId = companyId;
            ProductId = productId;
            Price = price;
            Quantity = quantity;
            Name = name;
            Description = description;
            Category = category;
        }
    }
}