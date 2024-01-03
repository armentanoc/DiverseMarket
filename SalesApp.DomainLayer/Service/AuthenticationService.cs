using SalesApp.DomainLayer.DTOs;
using SalesApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.DomainLayer.Service
{
    public static class AuthenticationService
    {
        public static LoginResponseDTO Login(LoginRequestDTO request)
        {
            (long? id, string? role) userInfo = UserDB.Login(request.username, request.password);

            return new LoginResponseDTO(userInfo.id, userInfo.role);
        }
    }
}
