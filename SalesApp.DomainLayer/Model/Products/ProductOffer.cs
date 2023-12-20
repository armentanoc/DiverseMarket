namespace SalesApp.DomainLayer.Model.Products
{

    internal class ProductOffer
    {
        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public int SellerId { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        //implementar imagem?

        internal ProductOffer(int productId, int sellerId, decimal price, int quantity)
        {
            this.Id = SetId();
            this.ProductId = productId;
            this.SellerId = sellerId;
            this.Price = price;
            this.Quantity = quantity;
        }

        private int SetId()
        {
            return Math.Abs(Guid.NewGuid().GetHashCode());
        }


        internal bool ChangePrice(decimal newPrice)
        {
            if(newPrice <= 0) return false;

            this.Price = newPrice;
            return true;
        }

        internal bool Sell(int quantity)
        {
            if (this.Quantity < quantity) return false;

            this.Quantity -= quantity; return true;
        }

        internal void AddMore(int quantity)
        {
            this.Quantity += quantity;
        }
    }
}
