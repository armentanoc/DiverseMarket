using DiverseMarket.Backend.Model.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiverseMarket.Backend.Model
{
    public class User
    {
        private long _id;
        private string? _username;
        private string? _name;
        private string? _email;
        private string? _password;
        private string? _phone;
        private Roles _role;
        private Address _address;

        public long Id { get { return _id; } }
        public string? Username { get { return _username; } }
        public string? Name { get { return _name; } }
        public string? Email { get { return _email; } }
        public string Phone { get { return _phone; } }
        public Roles Role { get { return _role; } }


        public User(long id, string username, string name, string email, string password, string phone, Roles role)
        {
            _id = id;
            _username = username;
            _name = name;
            _email = email;
            _phone = phone;
            _role = role;
            _password = password;
        }


    }
}
