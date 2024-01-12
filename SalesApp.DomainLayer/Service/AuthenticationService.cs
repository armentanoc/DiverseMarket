using SalesApp.DomainLayer.DTOs;
using SalesApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SalesApp.DomainLayer.Service
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
            if (UserDB.RegisterCustomer(registerCustomerDTO.FullName, registerCustomerDTO.Email, registerCustomerDTO.Username,
                registerCustomerDTO.Telephone, registerCustomerDTO.CPF,
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
