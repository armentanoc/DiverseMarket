using SalesApp.DomainLayer.Model.Enum;
using SalesApp.DomainLayer.Model.Products;

namespace SalesApp.DomainLayer.Model.Users
{
    public class Customer : User
    {
        private CustomerWallet _wallet;
        public List<ProductOffer> Cart { get; internal set; }
        public List<ProductOffer> AlreadyBoughtItems { get; internal set; } //TODO maybe link with a database

        public Customer(string username, string name, string email, string password, int phone) : base(username, name, email, password, phone)
        {
            _wallet = new CustomerWallet();
            Cart = new List<ProductOffer>();
            AlreadyBoughtItems = new List<ProductOffer>();
        }

        public void Buy(ProductOffer product, PaymentType paymentType)
        {
            Cart.Add(product);
            Buy(paymentType);
        }

        public void Buy(PaymentType paymentType)
        {
            if(Cart.Count == 0)
            {
                throw new Exception("Empty cart");
            }

            decimal totalAmount = Cart.Sum(product => product.Price);
            if(totalAmount > _wallet.Balance && paymentType == PaymentType.Debit)
            {
                throw new Exception("Insufficient funds.");
            }

            foreach(var product in Cart)
            {
                Pay(paymentType, product) ;
            }
        }

        private void Pay(PaymentType paymentType, ProductOffer product)
        {
            _wallet.Pay(product.Price, paymentType);
            product.Seller.CompleteSale(product, product.Price);
            AlreadyBoughtItems.Add(product);
            ClearCart();
        }

        public void ClearCart()
        {
            foreach (var item in Cart)
            {
                Cart.Remove(item);
            }
        }

        public decimal CheckBalance()
        {
            return _wallet.Balance;
        }

        public void RateProduct(ProductOffer product)
        {
            //choose product from already bought list
            //call ProductReview()
            throw new NotImplementedException();
        }

        public void RateSeller(Seller seller)
        {
            //choose seller based on product from already bought list
            //call SellerReviews()
            throw new NotImplementedException();
        }

        public void RequestRefund(ProductOffer product)
        {
            //choose product
            //call Refund()
            throw new NotImplementedException();
        }

        public void RequestHelp()
        {
            //contact moderator
            //send help message
            throw new NotImplementedException();
        }
    }
}
