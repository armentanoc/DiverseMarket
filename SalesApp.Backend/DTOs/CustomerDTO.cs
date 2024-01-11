using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.Backend.DTOs
{
    public class CustomerDTO
    {
        public long Id { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string Username { get; private set; }
        public string? Telephone { get; private set; }
        public string CPF { get; private set; }
        public AddressDTO Address { get; private set; }

        public CustomerDTO(long id, string fullName, string email, string username, string? telephone, string cPF, AddressDTO address)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            Username = username;
            Telephone = telephone;
            CPF = cPF;
            Address = address;
        }
    }
}
