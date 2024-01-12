namespace DiverseMarket.Backend.Model.Products
{

    public class ProductOffer
    {
        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public int SellerId { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        //implementar imagem?

        internal ProductOffer(int productId, int sellerId, decimal price, int quantity)
        {
            Id = SetId();
            ProductId = productId;
            SellerId = sellerId;
            Price = price;
            Quantity = quantity;
        }

        private int SetId()
        {
            return Math.Abs(Guid.NewGuid().GetHashCode());
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
