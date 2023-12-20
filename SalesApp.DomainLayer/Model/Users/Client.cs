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
        private List<dynamic> _productCart;
        private List<dynamic> _alreadyBought; //TODO maybe link with a database

        public Client(String username, String name, String email, String password, int phone)
        {
            _user = new User(username, name, email, password, phone);
            _wallet = new ClientWallet();
            _username = _user.Username;
            _name = _user.Name;
            _email = _user.Email;
            _password = _user.Password;
            _phone = _user.Phone;
            _productCart = new List<dynamic>();
            _alreadyBought = new List<dynamic>();
        }

        public void Buy(ProductSeller product)
        {
            //add all the products to a cart
            //calculate the total
            //ask for type of payment
            //call pay()
        }

        private void Pay(ProductSeller product)
        {
            //check value in wallet
            //if low, ask for deposit
            //complete sale with seller (product, payment)
            //reduce ammount from wallet
            //clear cart
        }

        public void Deposit(decimal ammount)
        {
            //add value to wallet total
        }

        public void CheckBalance()
        {
            //check total value from wallet
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
