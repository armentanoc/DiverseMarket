
namespace SalesApp.DomainLayer.Model.Users
{
    public class ClientWallet
    {
        public decimal Total { get; internal set; }

        internal void CheckBalance()
        {
            throw new NotImplementedException();
        }

        internal void Deposit(decimal ammount)
        {
            throw new NotImplementedException();
        }

        internal void Withdrawal(decimal total)
        {
            throw new NotImplementedException();
        }
    }
}
