using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiverseMarket.Backend.DTOs
{
    public class LoginRequestDTO
    {
        public string username { get; set; }
        public string password { get; set; }

        public LoginRequestDTO(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }
}
