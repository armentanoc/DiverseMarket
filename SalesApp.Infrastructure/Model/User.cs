using SalesApp.Infrastructure.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.Infrastructure.Model
{
    public class User
    {
        private long _id;
        private String? _username;
        private String? _name;
        private String? _email;
        private String? _password;
        private string? _phone;
        private Roles _role;
        private Address _address;

        public long Id { get { return _id; } }
        public String? Username { get { return _username; } }
        public String? Name { get { return _name; } }
        public String? Email { get { return _email; } }
        public string Phone { get { return _phone; } }
        public Roles Role { get { return _role; } }


        public User(long id, String username, String name, String email, String password, string phone, Roles role)
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
