using SalesApp.DomainLayer.Model.Enum;
using SalesApp.DomainLayer.Model.Products;

namespace SalesApp.DomainLayer.Model.Users
{
    public class Client
    {
        private User _user;
        private ClientWallet _wallet;
        private List<ProductSeller> _productCart;
        private List<ProductSeller> _alreadyBought; //TODO maybe link with a database

        public List<ProductSeller> Cart { get { return _productCart; } }
        public List<ProductSeller> AlreadyBought { get { return _alreadyBought; } }
        public String? Username { get { return _user.Username; } }
        public String? Name { get { return _user.Name; } }
        public String? Email { get { return _user.Email; } }
        public int Phone { get { return _user.Phone; } }
        public String? Password { get { return _user.Password; } }

        public Client(String username, String name, String email, String password, int phone, decimal startingBalance)
        {
            _user = new User(username, name, email, password, phone);
            _wallet = new ClientWallet(startingBalance);
            _productCart = new List<ProductSeller>();
            _alreadyBought = new List<ProductSeller>();
        }

        public void Buy(ProductSeller product, PaymentType paymentType)
        {
            _productCart.Add(product);
            Buy(paymentType);
        }

        public void Buy(PaymentType paymentType)
        {
            if(_productCart.Count == 0)
            {
                throw new Exception("Empty cart");
            }

            decimal totalAmount = _productCart.Sum(product => product.Price);
            if(totalAmount > _wallet.Balance && paymentType == PaymentType.Debit)
            {
                throw new Exception("Insufficient funds.");
            }

            foreach(var product in _productCart)
            {
                Pay(paymentType, product) ;
            }
        }

        private void Pay(PaymentType paymentType, ProductSeller product)
        {
            _wallet.Pay(product.Price, paymentType);
            product.Seller.CompleteSale(product, product.Price);

            foreach (var item in _productCart)
            {
                AlreadyBought.Add(item);
            }

            ClearCart();
        }

        public void ClearCart()
        {
            foreach (var item in Cart)
            {
                _productCart.Remove(item);
            }
        }

        public decimal CheckBalance()
        {
            return _wallet.Balance;
        }

        public void RateProduct(ProductSeller product)
        {
            //choose product from already bought list
            //call ProductReview()
        }

        public void RateSeller(Seller seller)
        {
            //choose seller based on product from already bought list
            //call SellerReviews()
        }

        public void RequestRefund(ProductSeller product)
        {
            //choose product
            //call Refund()
        }

        public void RequestHelp()
        {
            //contact moderator
            //send help message
        }
    }
}
