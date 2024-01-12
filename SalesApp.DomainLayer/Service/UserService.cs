using SalesApp.DomainLayer.DTOs;
using SalesApp.DomainLayer.Model.Users;
using SalesApp.Infrastructure.Model;
using SalesApp.Infrastructure.Operations;
using SalesApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.DomainLayer.Service
{
    public static class UserService
    {
        public static string GetUserFullNameById(long userId)
        {
            return UserDB.GetUserFullNameById(userId);
        }

        public static CustomerDTO GetCustomerById(long customerId)
        {
            User user = UserDB.GetUserById(customerId);
            string cpf = CustomerDB.GetCustomerCPFById(user.Id);
            Address address = AddressDB.GetAddressByUserId(user.Id);

            return new CustomerDTO(user.Id, user.Name, user.Email, user.Username, user.Phone, cpf, 
                new AddressDTO(address.ZipCode, address.Street, address.Complement, address.City, address.Number));
        }
    }
}
