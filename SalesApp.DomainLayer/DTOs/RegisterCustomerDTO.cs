using SalesApp.DomainLayer.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.DomainLayer.DTOs
{
    public class RegisterCustomerDTO
    {
        public string FullName {  get; private set; }
        public string Email {  get; private set; }
        public string Username {  get; private set; }
        public string? Telephone {  get; private set; }
        public string CPF { get; private set; } 
        public AddressDTO Address { get; private set; }
        public string Password { get; private set; }

        public RegisterCustomerDTO(string fullName, string email, string username, string? telephone, string cPF, AddressDTO address, string password)
        {
            FullName = fullName;
            Email = email;
            Username = username;
            Telephone = telephone;
            CPF = cPF;
            Address = address;
            Password = password;
        }
    }
}
