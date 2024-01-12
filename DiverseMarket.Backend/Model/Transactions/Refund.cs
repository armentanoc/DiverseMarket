using DiverseMarket.Backend.Model.Enums;

namespace DiverseMarket.Backend.Model.Transactions
{
    internal class Refund
    {
        public long Id { get; private set; }
        public long CustomerId { get; private set; }
        public long ProductId { get; private set; }
        public long CompanyId { get; private set; }
        public string CustomerComment {  get; private set; }
        public string SellerComment {  get; private set; }
        public string ModeratorComment {  get; private set; }
        public RefundStatus Status { get; private set; }
        public double TotalAmount { get; private set; }

        public Refund(long id, long customerId, long productId, long companyId, string customerComment, string sellerComment, string moderatorComment, RefundStatus status, double totalAmount)
        {
            Id = id;
            CustomerId = customerId;
            ProductId = productId;
            CompanyId = companyId;
            CustomerComment = customerComment;
            SellerComment = sellerComment;
            ModeratorComment = moderatorComment;
            Status = status;
            TotalAmount = totalAmount;
        }
    }
}
