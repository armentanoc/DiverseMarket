using DiverseMarket.Backend.DTOs;
using DiverseMarket.Backend.Infrastructure.Repositories;
using DiverseMarket.Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiverseMarket.Backend.Services
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
                new AddressDTO(address.ZipCode, address.Street, address.Complement, address.Neighborhood, address.City, address.Number));
        }

        public static bool UpdateUser(CustomerDTO updatedCustomer)
        {
            User savedUser = UserDB.GetUserById(updatedCustomer.Id);

            if (UserDB.UpdateUser(
                    updatedCustomer.Id,
                    updatedCustomer.Email,
                    updatedCustomer.Telephone))
                {
                    if(AddressDB.UpdateAddressByUserId(updatedCustomer.Id, updatedCustomer.Address))
                    {
                        return true;
                    }
                    else
                    {
                        UserDB.UpdateUser(savedUser.Id,
                                savedUser.Email,
                                savedUser.Phone);
                        return false; //undoing changes
                    }
                }
            return false;
                
        }
    }
}
