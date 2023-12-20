using SalesApp.DomainLayer.Model.Products;
using SalesApp.DomainLayer.Model.Users;

namespace SalesApp.DomainLayer.Model.Transactions
{
    public class Refund
    {
        private Client _client;
        private Seller _seller;
        private decimal _amount;
        private String _message;

        public Refund(ProductSeller product, Client client, String? message)
        {
            _client = client;
            _seller = product.Seller;
            _amount = product.Price;
            _message = message;
        }

        internal void RequestRefund()
        {
            throw new NotImplementedException();
            //maybe needs ModeratorForum implementation
            //add request to a moderator only list
            //wait for moderator to allow refund or not
        }

        internal void DisplayClientMessage()
        {
            Console.WriteLine(_message);
        }

        internal void ProcessRefund(bool moderatorAutorization)
        {
            if(moderatorAutorization)
            {
                _seller.Withdrawal(_amount);
                _client.Deposit(_amount);
            }
        }
    }
}
