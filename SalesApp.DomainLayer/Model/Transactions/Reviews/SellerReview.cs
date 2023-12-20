using SalesApp.DomainLayer.Model.Users;

namespace SalesApp.DomainLayer.Model.Transactions.Reviews
{
    internal class SellerReview : Review
    {

        public Seller Seller { get; private set; }
        public Client Client { get; private set; }
        public SellerReview(ReviewEnum reviewEnum, Seller seller, Client client, string? comment = null) : base(reviewEnum, comment)
        {
            Seller = seller;
            Client = client;
        }
    }
}
