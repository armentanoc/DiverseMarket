namespace DiverseMarket.Backend.Model.Products
{

    public class ProductOffer
    {
        public long Id { get; private set; }
        public long SellerId { get; private set; }
        public long ProductId { get; private set; }
        public decimal Price { get; private set; }
        public long Quantity { get; private set; }
        //implementar imagem?

        internal ProductOffer(long id, long productId, long sellerId, decimal price, long quantity)
        {
            Id = id;
            ProductId = productId;
            SellerId = sellerId;
            Price = price;
            Quantity = quantity;
        }

        internal bool ChangePrice(decimal newPrice)
        {
            if (newPrice <= 0) return false;

            Price = newPrice;
            return true;
        }

        internal bool Sell(int quantity)
        {
            if (Quantity < quantity) return false;

            Quantity -= quantity; return true;
        }

        internal void AddMore(int quantity)
        {
            Quantity += quantity;
        }
    }
}
