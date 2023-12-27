using SalesApp.DomainLayer.Model.Products;
using SalesApp.DomainLayer.Model.Users;

namespace SalesApp.DomainLayer.Model.Transactions
{
    public class Selling
    {
        public int? Id { get; set; }
        public Customer Customer { get;  set; }
        public DateTime SaleStartDate { get; set; }
        public DateTime? SaleEndDate { get; set; }
        public decimal TotalValue { get; set; }
       // public List<Refund> Refunds { get; private set; }
       // public bool hasRefund { get; private set; }
        public List<SellingItem> SellingItems {  get; set; } 
        public SellingStatus Status { get; set; }

        public Selling()
        {
             SellingItems = new List<SellingItem>();
        }

        public Selling(Customer customer)
        {
            Customer = customer;
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
