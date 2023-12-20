using SalesApp.DomainLayer.Model.Products;
using SalesApp.DomainLayer.Model.Users;

namespace SalesApp.DomainLayer.Model.Transactions
{
    public class Selling
    {
        public int? Id { get; private set; }
        public Client Client { get; private set; }
        public DateTime SaleStartDate { get; private set; }
        public DateTime? SaleEndDate { get; private set; }
        public decimal TotalValue { get; private set; }
        public List<ProductOffer> Products { get; private set; }
        public SellingStatus Status { get; private set; }

        public Selling()
        {
            Products = new List<ProductOffer>();
        }

        public Selling(Client client)
        {
            Client = client;
            SaleStartDate = DateTime.Now;
            TotalValue = CalculateTotalValue(); // Pode ser ajustado conforme necessário
            Products = new List<ProductOffer>();
            Status = SellingStatus.Pending;
        }

        private void UpdateTotalValue()
        {
            TotalValue = CalculateTotalValue();
        }

        public decimal CalculateTotalValue()
        {
            decimal total = 0.0m;

            if (Products != null)
            {
                foreach (ProductOffer productOffer in Products)
                {
                    total += productOffer.Quantity * productOffer.Price;
                }
            }
            return total;
        }


        public void AddProduct(ProductOffer product)
        {
            Products.Add(product);
            UpdateTotalValue();
        }

        public void RemoveProduct(ProductOffer product)
        {
            Products.Remove(product);
            UpdateTotalValue();
        }
        public void CompleteSale()
        {
            if (Status == SellingStatus.Pending)
            {
                Status = SellingStatus.Completed;
                SaleEndDate = DateTime.Now;
            }
        }
        public void CancelSale()
        {
            if (Status == SellingStatus.Pending || Status == SellingStatus.InProgress)
            {
                Status = SellingStatus.Canceled;
                SaleEndDate = DateTime.Now;
            }
        }

        public void RefundSale()
        {
            if (Status == SellingStatus.Pending || Status == SellingStatus.InProgress)
            {
                Status = SellingStatus.Refunded;
                SaleEndDate = DateTime.Now;
            }
        }


    }
        
}
