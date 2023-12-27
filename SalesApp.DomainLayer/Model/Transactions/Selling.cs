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
       // public List<Refund> Refunds { get; private set; }
       // public bool hasRefund { get; private set; }
        public List<SellingItem> SellingItems {  get; private set; } 
        public SellingStatus Status { get; private set; }

        public Selling()
        {
             SellingItems = new List<SellingItem>();
        }

        public Selling(Client client)
        {
            Client = client;
            SaleStartDate = DateTime.Now;
            TotalValue = CalculateTotalValue();
            SellingItems = new List<SellingItem>();
            Status = SellingStatus.Pending;
        }

        private void UpdateTotalValue()
        {
            TotalValue = CalculateTotalValue();
        }

        public decimal CalculateTotalValue()
        {
            decimal total = 0.0m;

            if (SellingItems != null)
            {
                foreach (SellingItem sellingItem in SellingItems)
                {
                    total += sellingItem.Quantity * sellingItem.Price;
                }
            }
            return total;
        }


        public void AddProduct(SellingItem item)
        {
            SellingItems.Add(item);
            UpdateTotalValue();
        }

        public void RemoveProduct(SellingItem item)
        {
            SellingItems.Remove(item);
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



    }
        
}
