using DiverseMarket.Backend.Model.Enums;

namespace DiverseMarket.Backend.Model.Transactions
{
    internal class Selling
    {
        public long Id { get; private set; }
        public long CustomerId { get; private set; }

        public DateTime Date { get; private set; }
        public OrderStatus Status { get; private set; }

        public double TotalAmount { get; private set; }

        public Selling(long id, long customerId, DateTime date, OrderStatus status, double totalAmount)
        {
            Id = id;
            CustomerId = customerId;
            Date = date;
            Status = status;
            TotalAmount = totalAmount;
        }
    }
}
