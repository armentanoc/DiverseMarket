using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.Backend.DTOs
{
    public class LoginResponseDTO
    {

        public long? Id { get; private set; }
        public string? UserRole { get; private set; }
        public LoginResponseDTO(long? id, string? userRole)
        {
            this.Id = id;
            this.UserRole = userRole;
        }
    }
}
