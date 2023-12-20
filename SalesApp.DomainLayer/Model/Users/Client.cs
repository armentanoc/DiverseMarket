using SalesApp.DomainLayer.Model.Products;

namespace SalesApp.DomainLayer.Model.Users
{
    public class Client
    {
        private User _user;
        private ClientWallet _wallet;
        private String? _username;
        private String? _name;
        private String? _email;
        private String? _password;
        private int _phone;
        private List<ProductSeller> _productCart;
        private List<dynamic> _alreadyBought; //TODO maybe link with a database

        public List<ProductSeller> Cart { get { return _productCart; } }
        public ClientWallet Wallet { get { return _wallet; } }

        public Client(String username, String name, String email, String password, int phone)
        {
            _user = new User(username, name, email, password, phone);
            _wallet = new ClientWallet();
            _username = _user.Username;
            _name = _user.Name;
            _email = _user.Email;
            _password = _user.Password;
            _phone = _user.Phone;
            _productCart = new List<ProductSeller>();
            _alreadyBought = new List<dynamic>();
        }

        public void AddToCart(ProductSeller product)
        {
            _productCart.Add(product);
        }

        public void RemoveFromCart(ProductSeller product)
        {
            _productCart.Remove(product);
        }

        public void ClearCart()
        {
            foreach (var item in Cart)
            {
                RemoveFromCart(item);
            }
        }

        public void Buy(ProductSeller product)
        {
            AddToCart(product);
            Buy();
        }

        public void Buy()
        {
            if(Cart.Count == 0)
            {
                throw new Exception("Carrinho vazio");
            }

            decimal totalPrice = Cart.Sum(product => product.Price);

            //ask for type of payment

            Pay(totalPrice);
        }

        private void Pay(decimal total)
        {
            if(Wallet.Total < total)
            {
                throw new Exception("Saldo insuficiente");
                //ask for deposit
            }

            //complete sale with seller (product, payment)

            Wallet.Withdrawal(total);

            ClearCart();
        }

        public void Deposit(decimal amount)
        {
            if(amount < 0)
            {
                throw new Exception("Valor invalido.");
            }
            Wallet.Deposit(amount);
        }

        public void CheckBalance()
        {
            Wallet.CheckBalance();
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
