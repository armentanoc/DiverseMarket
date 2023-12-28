using System.Security.Cryptography;
using System.Text;

namespace SalesApp.DomainLayer.Model.Users
{
    public class User
    {
        private int _id;
        private String? _username;
        private String? _name;
        private String? _email;
        private String? _password;
        private int _phone;
        private RolesEnum _role;
        private Address _address;

        public int Id { get; }
        public String? Username { get { return _username; } }
        public String? Name { get { return _name; } }
        public String? Email { get { return _email; } }
        public int Phone { get { return _phone; } }
        protected String? Password { get { return _password; } }
        public RolesEnum Role { get { return _role; } }
        private Address Address { get { return _address; } }


        public User(String username, String name, String email, String password, int phone, RolesEnum role, Address address)
        {
            _id = GenerateID();
            _username = username;
            _name = name;
            _email = email;
            _phone = phone;
            _role = role;
            _address = address;
            SetHashPassword(password);
        }

        private int GenerateID()
        {
            return Math.Abs(DateTime.Now.GetHashCode());
        }

        public void UpdatePassword(String oldPassword, String newPassword)
        {
            if (this.VerifyHashPassword(oldPassword))
            {
                SetHashPassword(newPassword);
            }
            else
            {
                throw new InvalidOperationException("Não foi possível atualizar a senha: Senha antiga errada.");
            }
        }

        public void ForgotPassword(String name, String newPassword)
        {
            if (name == Name)
            {
                SetHashPassword(newPassword);
            }
            else
            {
                throw new InvalidOperationException("Não foi possível mudar a senha: Nome errado.");
            }
        }

        private void SetHashPassword(String input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashedBytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                String hashPassword = builder.ToString();

                this._password = hashPassword;
            }
        }

        public bool VerifyHashPassword(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashedBytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString() == this._password;
            }
        }

        public void UpdatePhone(int phone)
        {
            this._phone = phone;
        }

    }
}
