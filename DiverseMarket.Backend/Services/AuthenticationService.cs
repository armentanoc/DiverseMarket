using DiverseMarket.Backend.DTOs;
using DiverseMarket.Backend.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiverseMarket.Backend.Services
{
    public static class AuthenticationService
    {
        public static bool IsUsernameAvailable(string username)
        {
            return UserDB.GetUserIdByUsername(username) == 0;
        }

        public static LoginResponseDTO Login(LoginRequestDTO request)
        {
            (long? id, string? role) userInfo = UserDB.Login(request.username, request.password);

            return new LoginResponseDTO(userInfo.id, userInfo.role);
        }

        public static LoginResponseDTO RegisterCustomer(RegisterCustomerDTO registerCustomerDTO)
        {
            if (UserDB.RegisterCustomer(
                registerCustomerDTO.FullName, 
                registerCustomerDTO.Email, 
                registerCustomerDTO.Username,
                registerCustomerDTO.Telephone, 
                registerCustomerDTO.CPF,
                registerCustomerDTO.Address.ZipCode,
                registerCustomerDTO.Address.Street,
                registerCustomerDTO.Address.Complement,
                registerCustomerDTO.Address.Number, registerCustomerDTO.Address.City, registerCustomerDTO.Password))
            {
                return Login(new LoginRequestDTO(registerCustomerDTO.Username, registerCustomerDTO.Password));
            }
            return new LoginResponseDTO(null, null);
        }
    }
}
