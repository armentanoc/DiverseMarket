
using SalesApp.DomainLayer.Model.Enum;

namespace SalesApp.DomainLayer.Model.Users
{
    public class ClientWallet
    {
        internal decimal Balance { get; set; }

        internal ClientWallet(decimal startingBalance)
        {
           Balance = startingBalance;
        }

        internal void Pay(decimal amount, PaymentType paymentType)
        {
            if (amount < 0)
            {
                throw new Exception("Invalid amount. Amount must be greater than 0.");
            }

            if (paymentType == PaymentType.Credit)
            {
                Credit(amount);
            }

            if(paymentType == PaymentType.Debit)
            {
                Debit(amount);
            }
        }

        private void Credit(decimal amount)
        {
            Balance += amount;
        }

        private void Debit(decimal amount)
        {
            if(amount > Balance)
            {
                throw new Exception("Insufficient funds.");
            }

            Balance -= amount;
        }
    }
}
